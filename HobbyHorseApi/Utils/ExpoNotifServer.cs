using Expo.Server.Client;
using Expo.Server.Models;

namespace HobbyHorseApi.Utils
{
    public static class ExpoNotifServer
    {
        public static async Task SendNotificationToFrontEndClients(PushApiClient pushClient, List<string> clientsPushTokens, string title, string bodyMessage)
        {
            Console.WriteLine($"SENDING NOTIFICATION => {title};\nTO:\n");
            foreach(string token in clientsPushTokens)
            {
                Console.WriteLine(token);
            };

            var pushTicketReq = new PushTicketRequest()
            {
                PushTo = clientsPushTokens,
                PushBadgeCount = 1,
                PushTitle = title,
                PushBody = bodyMessage
            };
            var result = await pushClient.PushSendAsync(pushTicketReq);
        }
    }
}
