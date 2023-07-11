
using System.Text.Json.Serialization;

namespace HobbyHorseApi.Entities
{
    public class Schedule
    {
        public string Id { get; set; }
        public List<Day> Days { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public string SkateProfileId { get; set; }
        public SkateProfile? SkateProfile { get; set; } = null;
        public List<Zone> Zones { get; set; }

        public int? MinimumAge { get; set; } = 12; 
        public int? MaximumAge { get; set; } = 90;
        public string Gender { get; set; }
        public int MaxNumberOfPeople { get; set; }
    }
}
