using System.Net.Http;
using System.Text.Json;
using SpaceExplorer.API.Models;
using SpaceExplorer.API.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SpaceExplorer.API.Dtos;

public class NasaDataService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;

    public NasaDataService(HttpClient httpClient, AppDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task FetchAndStorePlanetsAsync()
    {
        var url = "https://exoplanetarchive.ipac.caltech.edu/TAP/sync?query=SELECT+pl_name,disc_year,pl_rade,pl_bmasse,pl_orbper,sy_dist,discoverymethod+FROM+pscomppars&format=json";
        var response = await _httpClient.GetStringAsync(url);

        var planets = JsonSerializer.Deserialize<List<PlanetDto>>(response);

        var entities = planets.Select(p => new Planet
        {
            Name = p.pl_name,
            DiscoveryYear = p.disc_year,
            Radius = p.pl_rade,
            Mass = p.pl_bmasse,
            OrbitalPeriod = p.pl_orbper,
            DistanceFromEarth = p.sy_dist,
            DiscoveryMethod = p.discoverymethod
        });

        _context.Planets.AddRange(entities);
        await _context.SaveChangesAsync();

    }
}