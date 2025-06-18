using System.Net.Http;
using System.Text.Json;
using SpaceExplorer.API.Models;
using SpaceExplorer.API.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SpaceExplorer.API.Dtos;

public class NasaDataService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AppDbContext _context;

    public NasaDataService(IHttpClientFactory httpClientFactory, AppDbContext context)
    {
        _httpClientFactory = httpClientFactory;
        _context = context;
    }

    public async Task FetchAndStorePlanetsAsync()
    {
        var url = "https://exoplanetarchive.ipac.caltech.edu/TAP/sync?query=SELECT+pl_name,disc_year,pl_rade,pl_bmasse,pl_orbper,sy_dist,discoverymethod+FROM+pscomppars&format=json";
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetStringAsync(url);

        var planets = JsonSerializer.Deserialize<List<PlanetDto>>(response);

        var planetEntities = planets.Select(p => new Planet
        {
            Name = p.pl_name,
            DiscoveryYear = p.disc_year,
            Radius = p.pl_rade,
            Mass = p.pl_bmasse,
            OrbitalPeriod = p.pl_orbper,
            DistanceFromEarth = p.sy_dist,
            DiscoveryMethod = p.discoverymethod
        }).ToList();

        _context.Planets.AddRange(planetEntities);
        await _context.SaveChangesAsync();

        foreach (var planet in planetEntities)
        {
            //only run prediction if required fields exist
            if (planet.Mass == null || planet.Radius == null || planet.OrbitalPeriod == null)
                continue;

            var planetType = await PostForPrediction<string>("/predict-type", new
            {
                mass = planet.Mass,
                radius = planet.Radius,
                orbitalPeriod = planet.OrbitalPeriod
            });

            var atmosphere = await PostForPrediction<string>("/predict-atmosphere", new
            {
                mass = planet.Mass,
                radius = planet.Radius,
                orbitalPeriod = planet.OrbitalPeriod,
                planetType
            });

            var waterLikelihood = await PostForPrediction<double>("/predict-water", new
            {
                mass = planet.Mass,
                radius = planet.Radius,
                orbitalPeriod = planet.OrbitalPeriod,
                planetType,
                atmosphere
            });

            var bioScore = await PostForPrediction<double>("/predict-bioscore", new
            {
                mass = planet.Mass,
                radius = planet.Radius,
                orbitalPeriod = planet.OrbitalPeriod,
                atmosphere,
                waterLikelihood
            });

            await _context.SaveChangesAsync();
        }
    }

    public async Task<T> PostForPrediction<T>(string path, object payload) {
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var client = _httpClientFactory.CreateClient();


        var response = await client.PostAsync($"http://localhost:5001{path}", content);
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);
        var root = doc.RootElement;

        if (typeof(T) == typeof(String))
            return (T)(object)root.EnumerateObject().First().Value.GetString();
        if (typeof(T) == typeof(double))
            return (T)(object)root.EnumerateObject().First().Value.GetDouble();

        throw new InvalidOperationException("Unsupported return type.");
    }
}