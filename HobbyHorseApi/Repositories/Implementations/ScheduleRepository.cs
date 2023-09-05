using HobbyHorseApi.Entities;
using HobbyHorseApi.Entities.DBContext;
using HobbyHorseApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneSignalApi.Model;

namespace HobbyHorseApi.Repositories.Implementations
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly HobbyHorseContext _context;

        public ScheduleRepository(HobbyHorseContext context)
        {
            _context = context;
        }

        public async Task DeleteSchedule(string scheduleId)
        {
            try
            {
                var schedule = await _context.Schedules.FindAsync(scheduleId);
                if(schedule == null)
                {
                    throw new Exception("No schedule with this id found");
                }
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Schedule> PutSchedule(Schedule schedule, string scheduleId)
        {
            try
            {
                var oldSchedule = await _context.Schedules
                .Include(s => s.Days)
                .Include(s => s.Zones)
                .SingleOrDefaultAsync(s => s.Id == schedule.Id);


                if (oldSchedule == null)
                {
                    throw new Exception($"Schedule with id {schedule.Id} was not found");
                }

                List<Day> onlyNewDays = new List<Day>();

                //check if some days are already present in DB
                foreach (Day day in schedule.Days)
                {
                    Day alreadyExistingDay = await _context.Days.FindAsync(day.Id);
                    if(alreadyExistingDay != null)
                    {
                        //if day is already in db, we just attach it
                        _context.Days.Attach(alreadyExistingDay);
                        onlyNewDays.Add(alreadyExistingDay);

                    }
                    else
                    {
                        onlyNewDays.Add(day);
                    }
                }

                _context.Entry(oldSchedule).CurrentValues.SetValues(schedule);

                oldSchedule.Days.Clear();
                oldSchedule.Days.AddRange(onlyNewDays);

                oldSchedule.Zones.Clear();
                oldSchedule.Zones.AddRange(schedule.Zones);

                await _context.SaveChangesAsync();
                return schedule;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Schedule> GetSchedule(string scheduleId)
        {
            try
            {
                Schedule schedule = await _context.Schedules.Include(schedule => schedule.SkateProfile)
                    .ThenInclude(skateProfile => skateProfile.User)
                    .Include(schedule => schedule.Days)
                    .Include(schedule => schedule.Zones).ThenInclude(zone => zone.Location)
                    .FirstOrDefaultAsync(schedule => schedule.Id.Equals(scheduleId));

                return schedule;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Schedule>> GetScheduleForSkateProfile(string skateProfileId)
        {
            try
            {
                IEnumerable<Schedule> schedules = await _context.Schedules.Include(schedule => schedule.SkateProfile)
                    .ThenInclude(skateProfile => skateProfile.User)
                    .Include(schedule => schedule.Days)
                    .Include(schedule => schedule.Zones).ThenInclude(zone => zone.Location)
                    .Where(schedule => schedule.SkateProfile.Id.Equals(skateProfileId))
                    .ToListAsync();

                return schedules;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<IEnumerable<Schedule>> GetAllSchedules()
        {
            try
            {
                IEnumerable<Schedule> schedules = await _context.Schedules.Include(schedule => schedule.SkateProfile)
                    .ThenInclude(skateProfile => skateProfile.User)
                    .Include(schedule => schedule.Days)
                    .Include(schedule => schedule.Zones).ThenInclude(zone => zone.Location)
                    .ToListAsync();

                return schedules;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Schedule> PostSchedule(Schedule schedule)
        {
            try
            {

                if(schedule.Zones != null && schedule.Zones.Count > 0 && schedule.Zones[0].Location != null)
                {
                    string locationToPostId = schedule.Zones[0].Location.Id;
                    Location retrievedLocation = await _context.Locations.FirstOrDefaultAsync(location => location.Id.Equals(locationToPostId));

                    if(retrievedLocation != null)
                    {
                        _context.Locations.Attach(retrievedLocation);
                        schedule.Zones[0].Location = retrievedLocation;
                    }

                    await _context.Schedules.AddAsync(schedule);
                    await _context.SaveChangesAsync();
                    

                    //add skateProfile object withing object just so it can be verified if its from an aggresive skate profile
                    SkateProfile retrievedSkateProfile = await _context.SkateProfiles.FirstOrDefaultAsync(skateProfile => skateProfile.Id.Equals(schedule.SkateProfileId));

                    if(retrievedSkateProfile != null)
                    {
                        //set this to null to prevent cycles
                        retrievedSkateProfile.Events = null;
                        retrievedSkateProfile.RecommendedEvents = null;
                        retrievedSkateProfile.Schedules = null;
                        /////
                        
                        schedule.SkateProfile = retrievedSkateProfile;
                    }
                    else
                    {
                        throw new Exception("Schedule to post does not belong to any skateProfile");
                    }
                }
                else
                {
                    throw new Exception("There is no zone selected for this schedule");
                }
                
                return schedule;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        
    }
}
