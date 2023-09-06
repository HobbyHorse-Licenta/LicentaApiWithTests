using Google.Protobuf.WellKnownTypes;
using HobbyHorseApi.Entities;
using HobbyHorseApi.Entities.DBContext;
using HobbyHorseApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace HobbyHorseApi.Repositories.Implementations
{
    public class EventRepository : IEventRepository
    {
        private readonly HobbyHorseContext _context;

        public EventRepository(HobbyHorseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SkateProfile>> getParticipantsForEvent(string eventId)
        {
            return await _context.SkateProfiles
                .Include(skateProfile => skateProfile.User)
                .Include(skateProfile => skateProfile.AssignedSkills)
                .ThenInclude(assignedSkill => assignedSkill.Skill)
                .Where(skateProfile => skateProfile.Events.Any(evnt => evnt.Id == eventId)).ToListAsync();
        }

        public async Task<IEnumerable<SkateProfile>> getSuggestedParticipantsForEvent(string eventId)
        {
            return await _context.SkateProfiles
                .Include(skateProfile => skateProfile.User)
                .Include(skateProfile => skateProfile.AssignedSkills)
                .ThenInclude(assignedSkill => assignedSkill.Skill)
                .Where(skateProfile => skateProfile.RecommendedEvents.Any(evnt => evnt.Id == eventId)).ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetRecommendedEventsForSkateProfile(string skateProfileId)
        {
            try
            {
                IEnumerable<Event> events = await _context.Events
                  .Include(evnt => evnt.Outing).ThenInclude(outing => outing.Days)
                  .Include(evnt => evnt.SkateProfiles)
                  .Include(evnt => evnt.RecommendedSkateProfiles)
                  .Include(evnt => evnt.ScheduleRefrences)
                  .Include(evnt => evnt.Outing).ThenInclude(outing => outing.Trail)
                  .Where(evnt => evnt.RecommendedSkateProfiles.Any(skateProfile => String.Equals(skateProfile.Id, skateProfileId) ))
                  .ToListAsync();

                foreach (Event recommendedEvent in events)
                {
                    if (recommendedEvent.Outing.Trail.GetType() == typeof(ParkTrail))
                    {
                        recommendedEvent.Outing.Trail = await _context.Trails
                            .Include(trail => ((ParkTrail)trail).Location)
                            .FirstAsync(trail => trail.Id == recommendedEvent.Outing.Trail.Id);
                    }
                    else
                    {
                        recommendedEvent.Outing.Trail = await _context.Trails
                            .Include(trail => ((CustomTrail)trail).CheckPoints).ThenInclude(checkPoint => checkPoint.Location)
                            .FirstAsync(trail => trail.Id == recommendedEvent.Outing.Trail.Id);
                    }
                }

                return events;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public async Task<IEnumerable<Event>> GetEventsForSkateProfile(string skateProfileId)
        {

            try
            {
                List<Event> events = await _context.Events
                    .Include(evnt => evnt.Outing).ThenInclude(outing => outing.Trail)
                    .Include(evnt => evnt.Outing).ThenInclude(outing => outing.Days)
                    .Include(evnt => evnt.SkateProfiles)
                    .Include(evnt => evnt.ScheduleRefrences)
                    .Include(evnt => evnt.RecommendedSkateProfiles)
                    .Where(evnt => evnt.SkateProfiles.Any(skateProfile => String.Equals(skateProfile.Id, skateProfileId) )).ToListAsync();
                
                foreach (Event evnt in events)
                {
                    if (evnt.Outing.Trail.GetType() == typeof(ParkTrail))
                    {
                        evnt.Outing.Trail = await _context.Trails
                            .Include(trail => ((ParkTrail)trail).Location)
                            .FirstAsync(trail => trail.Id == evnt.Outing.Trail.Id);
                    }
                    else
                    {
                        evnt.Outing.Trail = await _context.Trails
                            .Include(trail => ((CustomTrail)trail).CheckPoints).ThenInclude(checkPoint => checkPoint.Location)
                            .FirstAsync(trail => trail.Id == evnt.Outing.Trail.Id);
                    }
                }

                return events;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsForUser(string userId)
        {
            try
            {
                List<Event> events = await _context.Events
                    .Include(evnt => evnt.Outing).ThenInclude(outing => outing.Trail)
                    .Include(evnt => evnt.Outing).ThenInclude(outing => outing.Days)
                    .Include(evnt => evnt.SkateProfiles)
                    .Include(evnt => evnt.ScheduleRefrences)
                    .Include(evnt => evnt.RecommendedSkateProfiles)
                    .Where(evnt => evnt.SkateProfiles.Any(skateProfile => String.Equals(skateProfile.UserId, userId))).ToListAsync();

                foreach (Event evnt in events)
                {
                    if (evnt.Outing.Trail.GetType() == typeof(ParkTrail))
                    {
                        evnt.Outing.Trail = await _context.Trails
                            .Include(trail => ((ParkTrail)trail).Location)
                            .FirstAsync(trail => trail.Id == evnt.Outing.Trail.Id);
                    }
                    else
                    {
                        evnt.Outing.Trail = await _context.Trails
                            .Include(trail => ((CustomTrail)trail).CheckPoints).ThenInclude(checkPoint => checkPoint.Location)
                            .FirstAsync(trail => trail.Id == evnt.Outing.Trail.Id);
                    }
                }

                return events;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



        public async Task<Event> PostEvent(Event evnt)
        {
            try
            {
                //look for the recommended skateProfiles
                List<SkateProfile> recommendedSkateProfilesFromDB = new List<SkateProfile>();
                foreach(SkateProfile searchedSkateProfile in evnt.RecommendedSkateProfiles)
                {
                    SkateProfile foundSkateProfile = _context.SkateProfiles.Find(searchedSkateProfile.Id);
                    if (foundSkateProfile != null)
                    {
                        recommendedSkateProfilesFromDB.Add(foundSkateProfile);
                    }
                }
                if(recommendedSkateProfilesFromDB.Count > 0)
                {
                    _context.SkateProfiles.AttachRange(recommendedSkateProfilesFromDB);
                    evnt.RecommendedSkateProfiles = recommendedSkateProfilesFromDB;
                }

                ///look for the participating skateProfiles
                List<SkateProfile> skateProfilesFromDB = new List<SkateProfile>();
                foreach (SkateProfile searchedSkateProfile in evnt.SkateProfiles)
                {
                    SkateProfile foundSkateProfile = _context.SkateProfiles.Find(searchedSkateProfile.Id);
                    if (foundSkateProfile != null)
                    {
                        skateProfilesFromDB.Add(foundSkateProfile);
                    }
                }
                if (skateProfilesFromDB.Count > 0)
                {
                    _context.SkateProfiles.AttachRange(skateProfilesFromDB);
                    evnt.SkateProfiles = skateProfilesFromDB;
                }



                ///look for existing trail in outing
                ParkTrail foundparkTrail = _context.ParkTrails.Find(evnt.Outing.Trail.Id);
                if (foundparkTrail != null)
                {
                    _context.ParkTrails.Attach(foundparkTrail);
                    evnt.Outing.Trail = foundparkTrail;
                }

                await _context.Events.AddAsync(evnt);
                await _context.SaveChangesAsync();
                return evnt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw;
            }
        }

        public async Task<Event> PutEvent(Event evnt)
        {
            try
            {
                var oldEvent = await _context.Users.FindAsync(evnt.Id);
                if (oldEvent == null)
                {
                    throw new Exception($"Event with id {evnt.Id} was not found");
                }


                for(int i = 0; i < evnt.RecommendedSkateProfiles.Count; i++)
                {
                    SkateProfile currentSkateProfile = evnt.RecommendedSkateProfiles[i];

                    List<Schedule> schedules  = await _context.Schedules.Include(schedule => schedule.Days).
                            Include(schedule => schedule.Zones).Where(
                        schedule => schedule.SkateProfileId == currentSkateProfile.Id).ToListAsync();
                    
                    if (schedules != null)
                    {
                        evnt.RecommendedSkateProfiles[i].Schedules = schedules;
                        _context.Schedules.AttachRange(schedules);
                    }

                }
                Console.WriteLine("EVENTREPO: "/* + JsonSerializer.Serialize(evnt)*/);
                _context.Entry(oldEvent).CurrentValues.SetValues(evnt);
                await _context.SaveChangesAsync();
                return evnt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteEvent(string eventId)
        {
            try
            {
                var evnt = await _context.Events.FindAsync(eventId);
                if (evnt == null)
                {
                    throw new Exception("No event with this id found");
                }
                _context.Events.Remove(evnt);
                await _context.SaveChangesAsync();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            try
            {
                IEnumerable<Event> events = await _context.Events
                                        .Include(evnt => evnt.RecommendedSkateProfiles)
                                        .Include(evnt => evnt.SkateProfiles)
                                        .Include(evnt => evnt.ScheduleRefrences)
                                        .Include(evnt => evnt.Outing).ThenInclude(outing => outing.Days)
                                        .Include(evnt => evnt.Outing).ThenInclude(outing => outing.Trail)
                                        .ToListAsync();

                foreach (Event evnt in events)
                {

                    for(int i = 0; i < evnt.SkateProfiles.Count; i++)
                    {
                        evnt.SkateProfiles[i].Events = null;
                        evnt.SkateProfiles[i].Schedules = null;
                    }

                    for (int i = 0; i < evnt.RecommendedSkateProfiles.Count; i++)
                    {
                        evnt.RecommendedSkateProfiles[i].Events = null;
                        evnt.RecommendedSkateProfiles[i].Schedules = null;
                    }

                    if (evnt.Outing.Trail.GetType() == typeof(ParkTrail))
                    {
                        evnt.Outing.Trail = await _context.Trails
                            .Include(trail => ((ParkTrail)trail).Location)
                            .FirstAsync(trail => trail.Id == evnt.Outing.Trail.Id);
                    }
                    else
                    {
                        evnt.Outing.Trail = await _context.Trails
                            .Include(trail => ((CustomTrail)trail).CheckPoints).ThenInclude(checkPoint => checkPoint.Location)
                            .FirstAsync(trail => trail.Id == evnt.Outing.Trail.Id);
                    }
                }


                return events;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Event> JoinSkateProfile(string eventId, string skateProfileId)
        {
            try
            {
                var evnt = await _context.Events
                    .Include(evnt => evnt.SkateProfiles)
                    .Include(evnt => evnt.RecommendedSkateProfiles)
                    .FirstOrDefaultAsync(evnt => evnt.Id == eventId);

                if (evnt == null)
                {
                    throw new Exception($"Event with id {eventId} was not found");
                }

                var skateProfile = evnt.RecommendedSkateProfiles.FirstOrDefault(skateProf => skateProf.Id == skateProfileId);
                if (skateProfile == null)
                {
                    throw new Exception($"SkateProfile with id {skateProfileId} was not found in event");
                }

                evnt.RecommendedSkateProfiles.Remove(skateProfile);
                evnt.SkateProfiles.Add(skateProfile);
                await _context.SaveChangesAsync();

                return evnt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Event> LeaveSkateProfile(string eventId, string skateProfileId)
        {
            try
            {
                var evnt = await _context.Events
                    .Include(evnt => evnt.SkateProfiles)
                    .Include(evnt => evnt.RecommendedSkateProfiles)
                    .FirstOrDefaultAsync(evnt => evnt.Id == eventId);

                if (evnt == null)
                {
                    throw new Exception($"Event with id {eventId} was not found");
                }

                var skateProfile = evnt.SkateProfiles.FirstOrDefault(skateProf => skateProf.Id == skateProfileId);
                if (skateProfile == null)
                {
                    throw new Exception($"SkateProfile with id {skateProfileId} was not found in event");
                }

                evnt.SkateProfiles.Remove(skateProfile);
                evnt.RecommendedSkateProfiles.Add(skateProfile);
                await _context.SaveChangesAsync();

                return evnt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        
    }
}
