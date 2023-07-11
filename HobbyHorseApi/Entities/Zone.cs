namespace HobbyHorseApi.Entities
{
    public class Zone
    {
        public string Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public double Range { get; set; }
        public string ScheduleId { get; set; }
        public Schedule? Schedule { get; set; } = null;
        public Location Location { get; set; }
        public string LocationId { get; set; }
    }
}
