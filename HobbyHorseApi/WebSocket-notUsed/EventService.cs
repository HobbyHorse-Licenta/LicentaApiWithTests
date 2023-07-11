using Newtonsoft.Json;
using System.Text;

namespace HobbyHorseApi.WebSocket_notUsed
{
    public class EventService : IEventService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendEvent(string eventName, object eventData)
        {
            var sseEventString = $"data: Ciao hello!\n\n";
            var sseEventBytes = Encoding.UTF8.GetBytes(sseEventString);

            var response = _httpContextAccessor.HttpContext.Response;
            await response.Body.WriteAsync(sseEventBytes, 0, sseEventBytes.Length);
            await response.Body.FlushAsync();
        }
    }
}
