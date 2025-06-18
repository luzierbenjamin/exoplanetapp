namespace SpaceExplorer.API.Models
{
    public class Planet
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DiscoveryMethod { get; set; }
        public int? DiscoveryYear { get; set; }
        public double? Radius { get; set; }
        public double? Mass { get; set; }
        public double? OrbitalPeriod { get; set; }
        public double? DistanceFromEarth { get; set; }

        public AIResult AIResult { get; set; } 
    }
}