using HobbyHorseApi.Entities;
using HobbyHorseApi.Entities.DBContext;
using HobbyHorseApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HobbyHorseApi.Repositories.Implementations
{
    public class LocationRepository : ILocationRepository
    {
        private readonly HobbyHorseContext _context;

        public LocationRepository(HobbyHorseContext context)
        {
            _context = context;
        }

        public async Task<Location> GetLocationByName(string name)
        {
            try
            {
                var location = await _context.Locations.Where((loc) => loc.Name == name).FirstOrDefaultAsync();
                if (location == null)
                {
                    throw new Exception($"Location '{name}' not found");
                }
                return location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
