using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceExplorer.API.Data;

[Authorize]
[Route("api/[controller]")]
public class PlanetsController : ControllerBase
{
    private readonly AppDbContext _context;
    public PlanetsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlanets()
    {
        var planets = await _context.Planets.Take(50).ToListAsync(); // only 50 for now
        return Ok(planets);
    }

    [HttpGet("enriched")]
    public async Task<IActionResult> GetEnrichedPlanets()
    {
        var data = await _context.Planets
            .Include(p => p.AIResult)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.DiscoveryYear,
                p.Mass,
                p.Radius,
                p.OrbitalPeriod,
                Predictions = new
                {
                    p.AIResult.PlanetType,
                    p.AIResult.Atmoshpere,
                    p.AIResult.WaterLikelihood,
                    p.AIResult.BioScore
                }
            })
            .ToListAsync();

        return Ok(data);
    }
}