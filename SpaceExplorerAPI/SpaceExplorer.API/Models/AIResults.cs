namespace SpaceExplorer.API.Models
{
    public class AIResult
    {
        public int Id { get; set; }
        public int PlanetID { get; set; }
        public Planet Planet { get; set; }
        public string PlanetType { get; set; }
        public string Atmoshpere { get; set; }
        public double? WaterLikelihood { get; set; }
        public double? BioScore { get; set; }
    }
}