using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Interfaces
{
    public interface ISkateProfileService
    {
        Task<SkateProfile> PostSkateProfile(SkateProfile skateProfile);
        Task<List<SkateProfile>> getAllSkateProfiles();
        Task<SkateProfile> getSkateProfile(string skateProfileId);


    }
}
