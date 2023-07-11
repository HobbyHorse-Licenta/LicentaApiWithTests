using Newtonsoft.Json;

namespace HobbyHorseApi.Entities
{
    public class CustomTrail : Trail
    {
        //[JsonConstructor]
        //public CustomTrail()
        //{

        //}
        public List<CheckPoint> CheckPoints { get; set; }
    }
}
