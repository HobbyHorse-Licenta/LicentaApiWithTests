using WebSocketSharp;
using WebSocketSharp.Server;

namespace HobbyHorseApi.WebSocket
{
    public class EventNotifier : WebSocketBehavior
    {
        //protected override void OnMessage(MessageEventArgs e)
        //{
        //    Console.WriteLine(e.Data);

        //    //Send("Am primit");
        //}
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine(e.Data);

            //    //Send("Am primit");
        }


    }
}
