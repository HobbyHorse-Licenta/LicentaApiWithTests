using HobbyHorseApi.JsonConverters;
using Newtonsoft.Json;
using System.Collections;

namespace HobbyHorseApi.Entities
{
    public class Event
    {
        //public Event()
        //{

        //}
        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int MaxParticipants { get; set; }
        public string SkateExperience { get; set; }

        [JsonConverter(typeof(OutingConverter))]

        public Outing Outing { get; set; }
        public List<SkateProfile>? SkateProfiles { get; set; } = new List<SkateProfile>();
        public List<SkateProfile> RecommendedSkateProfiles { get; set; } = new List<SkateProfile>();
        public List<ScheduleRefrence> ScheduleRefrences { get; set; } = new List<ScheduleRefrence>();
        public string? ImageUrl { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty;
        public string Gender { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }

    }
}
