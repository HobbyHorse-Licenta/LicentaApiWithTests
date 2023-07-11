using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetRecommendedEventsForSkateProfile(string skateProfileId);
        Task<Event> PostEvent(Event evnt);
        Task<Event> PutEvent(Event evnt);
        Task<IEnumerable<SkateProfile>> getParticipantsForEvent(string eventId);
        Task<IEnumerable<SkateProfile>> getSuggestedParticipantsForEvent(string eventId);
        Task DeleteEvent(string eventId);
        Task<IEnumerable<Event>> GetAllEvents();

        Task<Event> JoinSkateProfile(string eventId, string skateProfileId);
        Task<Event> LeaveSkateProfile(string eventId, string skateProfileId);

        Task<IEnumerable<Event>> GetEventsForSkateProfile(string skateProfileId);

    }

}
