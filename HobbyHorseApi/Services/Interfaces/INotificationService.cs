using HobbyHorseApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Services.Interfaces
{
    public interface INotificationService
    {
        Task<string> GetNotificationTokenForSkateProfile(string skateProfileId);
    }

}
