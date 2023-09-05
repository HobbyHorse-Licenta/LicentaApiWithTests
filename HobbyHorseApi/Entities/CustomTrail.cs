using Newtonsoft.Json;

namespace HobbyHorseApi.Entities
{
    public class CustomTrail : Trail
    {
        public List<CheckPoint> CheckPoints { get; set; }
    }
}
