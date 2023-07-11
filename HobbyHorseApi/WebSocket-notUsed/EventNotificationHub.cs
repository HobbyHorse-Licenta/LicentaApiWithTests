using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;

namespace HobbyHorseApi.WebSocket
{
    public class EventNotificationHub : Hub
    {
        public async Task SendMessage(string message)
        {
            Console.WriteLine("Received message");
            //await Clients.All.SendAsync("ReceiveMessage", message);
        }

    }
}
