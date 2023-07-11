using HobbyHorseApi.Entities;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Interfaces;

namespace HobbyHorseApi.Services.Implementations
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<Location> GetLocationByName(string name)
        {
            try
            {
                return await _locationRepository.GetLocationByName(name); 
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
