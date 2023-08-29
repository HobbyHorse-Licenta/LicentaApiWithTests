using HobbyHorseApi.Entities;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace HobbyHorseApi.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;
        public NotificationService(INotificationRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> GetNotificationTokenForSkateProfile(string skateProfileId)
        {
            try
            {
                SkateProfile skateprofile = await _repo.GetSkateProfilePlusNotifToken(skateProfileId);
                return skateprofile.User.PushNotificationToken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
