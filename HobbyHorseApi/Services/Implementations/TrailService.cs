using HobbyHorseApi.Entities;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Interfaces;

namespace HobbyHorseApi.Services.Implementations
{
    public class TrailService : ITrailService
    {

        private readonly ITrailRepository _trailRepo;

        public TrailService(ITrailRepository trailRepo)
        {
            _trailRepo = trailRepo;
        }

        public async Task<IEnumerable<ParkTrail>> GetAllParkTrails()
        {
            try
            {
                return await _trailRepo.GetAllParkTrails();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
