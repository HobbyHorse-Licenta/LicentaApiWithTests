using HobbyHorseApi.JsonConverters;
using Newtonsoft.Json;

namespace HobbyHorseApi.Entities
{
    [JsonConverter(typeof(OutingConverter))]
    public class Outing
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public List<Day> Days { get; set; }
        //public Day? VotedDay { get; set; }
        public string? VotedDayId { get; set; }

        public double VotedStartTime { get; set; }
        public string SkatePracticeStyle { get; set; }
        public Trail Trail { get; set; }
        public bool Booked { get; set; }

    }
}
