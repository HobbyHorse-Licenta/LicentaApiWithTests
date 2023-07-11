using System.Text.Json.Serialization;

namespace HobbyHorseApi.Entities
{
    public class SkateProfile
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; } = null;

        public List<Event>? Events { get; set; } = new List<Event>();
        [JsonIgnore]
        public List<Event>? RecommendedEvents { get; set; } = new List<Event>();
        public List<AssignedSkill>? AssignedSkills { get; set; } = new List<AssignedSkill>();
        public List<Schedule>? Schedules { get; set; } = new List<Schedule>();
        public string SkateType { get; set; }
        public string SkatePracticeStyle { get; set; }
        public string SkateExperience { get; set; }

    }
}
