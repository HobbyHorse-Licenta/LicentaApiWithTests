using System.Text;
using WebSocketSharp.Server;

namespace HobbyHorseApi.WebSocket
{
    public class WebSocketService :  BackgroundService
    {
        public static int webSocketPort = int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "5000");
        public static WebSocketServer wssv = new WebSocketServer(webSocketPort);
        //public static HttpServer httpsv = new HttpServer(4649);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return;
            }
            //httpsv.AddWebSocketService<EventNotifier>("/notify");
            //wssv.AddWebSocketService<EventNotifier>("/EventNotifications");
            // wssv.Start();
            //httpsv.Start();
            Thread.Sleep(10000);
            Console.WriteLine("Sending smth to frontend");


            //string eventString = $"data: Ciao mai!\n\n";
            //byte[] eventBytes = Encoding.UTF8.GetBytes(eventString);

            //await HttpContext.Response.Body.WriteAsync(eventBytes, 0, eventBytes.Length);
            //await HttpContext.Response.Body.FlushAsync();
            //await Task.Delay(1000); // wait before sending the next event

            Thread.Sleep(-1);
        }

        public async Task Sse(HttpContext context)
        {
            context.Response.ContentType = "text/event-stream";
            context.Response.Headers.Add("Cache-Control", "no-cache");
            context.Response.Headers.Add("Connection", "keep-alive");
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            await context.Response.Body.FlushAsync();

            while (true)
            {
                //string eventData = GetEventData(); // retrieve data for the next event
                //if (string.IsNullOrEmpty(eventData))
                //{
                //    await Task.Delay(1000); // wait for new data to be available
                //    continue;
                //}

                
            }
        }
    }
}
