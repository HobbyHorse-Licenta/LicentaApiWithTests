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

        public static async Task SendNotificationToUsersWithNotifToken(List<string> notificationTokens, string messageTitle, string message)
        {
            if (notificationTokens != null && notificationTokens.Count > 0)
            {
                try
                {
                    await ExpoNotifServer.SendNotificationToFrontEndClients(_pushApiClient, notificationTokens, messageTitle, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No notification tokens provided to send notifications to");
            }
        }
        public static async Task SendNotificationToUsersWithSkateProfiles(List<SkateProfile> skateProfiles, string messageTitle, string message)
        {
            List<string> clients = new List<string>();
            if (skateProfiles == null || skateProfiles.Count == 0)
                return;
            try
            {
                foreach (SkateProfile skateProfile in skateProfiles)
                {
                    if (skateProfile.User != null && skateProfile.User.PushNotificationToken != null && skateProfile.User.PushNotificationToken.Length > 0)
                    {
                        clients.Add(skateProfile.User.PushNotificationToken);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (clients.Count > 0)
            {
                try
                {
                    await ExpoNotifServer.SendNotificationToFrontEndClients(_pushApiClient, clients, messageTitle, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No users to send notifications to, maybe they didnt allow for notifications");
            }
           
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
