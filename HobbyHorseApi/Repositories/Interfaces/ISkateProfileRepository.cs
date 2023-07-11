using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HobbyHorseApi.Repositories.Interfaces
{
    public interface ISkateProfileRepository
    {
        Task<SkateProfile> PostSkateProfile(SkateProfile skateProfile);
        Task<List<SkateProfile>> getAllSkateProfiles();
        Task<SkateProfile> getSkateProfile(string skateProfileId);
    }
}
