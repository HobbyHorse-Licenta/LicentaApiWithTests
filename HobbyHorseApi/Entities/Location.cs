namespace HobbyHorseApi.Entities
{
    public class Location
    {
        public string Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public string? ImageUrl { get; set; } = String.Empty;
        public double Lat { get; set; }
        public double Long { get; set; }

        public List<Zone>? Zones { get; set; } = new List<Zone>();
    }
}
