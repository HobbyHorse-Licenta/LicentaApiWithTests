using OneSignalApi.Client;
using Expo.Server.Client;
using OneSignalApi.Api;
using OneSignalApi.Model;
using HobbyHorseApi.Entities;
using HobbyHorseApi.Services.Interfaces;
using HobbyHorseApi.Controllers;
using HobbyHorseApi.Services.Implementations;
using HobbyHorseApi.Repositories.Implementations;
using User = HobbyHorseApi.Entities.User;

namespace HobbyHorseApi.Utils
{
    public static class NotificationUtil
    {

        private static readonly PushApiClient _pushApiClient;
        static NotificationUtil()
        {
            _pushApiClient = new PushApiClient();
        }
        public static async Task SendNotificationToUsersWithSkateProfiles(List<SkateProfile> skateProfiles, string messageTitle, string message)
        {
            List<string> clients = new List<string>();
            if (skateProfiles == null || skateProfiles.Count == 0)
                return;
            foreach (SkateProfile skateProfile in skateProfiles)
            {
                if (skateProfile.User != null && skateProfile.User.PushNotificationToken != null && skateProfile.User.PushNotificationToken.Length > 0)
                {
                    clients.Add(skateProfile.User.PushNotificationToken);
                }
            }
            await ExpoNotifServer.SendNotificationToFrontEndClients(_pushApiClient, clients, messageTitle, message);
        }

        public static async Task SendNotificationToUsersWithUserIds(IUserService userService, List<string> userIds, string messageTitle, string message)
        {
            
            var users = await userService.GetAllUsers();
            var clients = new List<string>();
            foreach (User user in users)
            {
                if(user.PushNotificationToken != null && user.PushNotificationToken.Length > 0)
                {
                    clients.Add(user.PushNotificationToken);
                }
            }
            await ExpoNotifServer.SendNotificationToFrontEndClients(_pushApiClient, clients, messageTitle, message);
        }
    }
}
