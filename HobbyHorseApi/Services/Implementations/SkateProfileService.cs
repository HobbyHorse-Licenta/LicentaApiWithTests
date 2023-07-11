using HobbyHorseApi.Entities;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Implementations
{
    public class SkateProfileService : ISkateProfileService
    {
        private readonly ISkateProfileRepository _skateProfileRepository;
        public SkateProfileService(ISkateProfileRepository skateProfileRepository)
        {
            _skateProfileRepository = skateProfileRepository;
        }

        public async Task<List<SkateProfile>> getAllSkateProfiles()
        {
            try
            {
                return await _skateProfileRepository.getAllSkateProfiles();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<SkateProfile> getSkateProfile(string skateProfileId)
        {
            try
            {
                return await _skateProfileRepository.getSkateProfile(skateProfileId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<SkateProfile> PostSkateProfile(SkateProfile skateProfile)
        {
            try
            {
                return await _skateProfileRepository.PostSkateProfile(skateProfile);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
