namespace SpaceExplorer.API.Dtos
{

    public class PlanetDto
    {
        public string? pl_name { get; set; }
        public int? disc_year { get; set; }
        public double? pl_rade { get; set; }
        public double? pl_bmasse { get; set; }
        public double? pl_orbper { get; set; }
        public double? sy_dist { get; set; }
        public string? discoverymethod { get; set; }
        public string? st_spectype { get; set; }
    }
}