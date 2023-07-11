using HobbyHorseApi.Entities;

namespace HobbyHorseApi.Repositories.Interfaces
{
    public interface ITrailRepository
    {
        Task<IEnumerable<ParkTrail>> GetAllParkTrails();
    }
}
