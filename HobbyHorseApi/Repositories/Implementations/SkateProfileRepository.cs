using HobbyHorseApi.Entities;
using HobbyHorseApi.Entities.DBContext;
using HobbyHorseApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HobbyHorseApi.Repositories.Implementations
{
    public class SkateProfileRepository : ISkateProfileRepository
    {
        private readonly HobbyHorseContext _context;

        public SkateProfileRepository(HobbyHorseContext context)
        {
            _context = context;
        }

        public async Task<List<SkateProfile>> getAllSkateProfiles()
        {
            try
            {
                var skateProfiles = await _context.SkateProfiles
                .Include(skateProfile => skateProfile.Schedules)
                .Include(skateProfile => skateProfile.User).ToListAsync();
                return skateProfiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<SkateProfile> getSkateProfile(string skateProfileId)
        {
            try
            {
                var skateProfile = await _context.SkateProfiles.Include(skateProfile => skateProfile.Schedules)
                .Include(skateProfile => skateProfile.User).Where(skateProfile => skateProfile.Id == skateProfileId).FirstOrDefaultAsync();
                
                return skateProfile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw;
            }
        }

        public async Task<SkateProfile> PostSkateProfile(SkateProfile skateProfile)
        {
            try
            {
                await _context.SkateProfiles.AddAsync(skateProfile);
                await _context.SaveChangesAsync();
                return skateProfile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
