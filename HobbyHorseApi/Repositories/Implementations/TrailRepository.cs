using HobbyHorseApi.Entities;
using HobbyHorseApi.Entities.DBContext;
using HobbyHorseApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HobbyHorseApi.Repositories.Implementations
{
    public class TrailRepository : ITrailRepository
    {
        private readonly HobbyHorseContext _context;

        public TrailRepository(HobbyHorseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParkTrail>> GetAllParkTrails()
        {
            try
            {
                IEnumerable<ParkTrail> parkTrails = await _context.ParkTrails.Include(parkTrail => parkTrail.Location)
                                                                       .ToListAsync();

                return parkTrails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
