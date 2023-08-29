using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<SkateProfile> GetSkateProfilePlusNotifToken(string skateProfileId);
    }
}
