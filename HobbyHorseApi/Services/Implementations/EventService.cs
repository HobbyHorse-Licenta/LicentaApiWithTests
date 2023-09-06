using HobbyHorseApi.Entities;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace HobbyHorseApi.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;
        public EventService(IEventRepository repo)
        {
            _repo = repo;
        }

        public async Task DeleteEvent(string eventId)
        {
            try
            {
                await _repo.DeleteEvent(eventId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            try
            {
                return await _repo.GetAllEvents();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SkateProfile>> getParticipantsForEvent(string eventId)
        {
            try
            {
                return await _repo.getParticipantsForEvent(eventId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetRecommendedEventsForSkateProfile(string skateProfileId)
        {
            try
            {
                return await _repo.GetRecommendedEventsForSkateProfile(skateProfileId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsForSkateProfile(string skateProfileId)
        {
            try
            {
                return await _repo.GetEventsForSkateProfile(skateProfileId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SkateProfile>> getSuggestedParticipantsForEvent(string eventId)
        {
            try
            {
                return await _repo.getSuggestedParticipantsForEvent(eventId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Event> JoinSkateProfile(string eventId, string skateProfileId)
        {
            try
            {
                return await _repo.JoinSkateProfile(eventId, skateProfileId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Event> LeaveSkateProfile(string eventId, string skateProfileId)
        {
            try
            {
                return await _repo.LeaveSkateProfile(eventId, skateProfileId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Event> PostEvent(Event evnt)
        {
            try
            {
                return await _repo.PostEvent(evnt);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Event> PutEvent(Event evnt)
        {
            try
            {
                return await _repo.PutEvent(evnt);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsForUser(string userId)
        {
            try
            {
                return await _repo.GetEventsForUser(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
