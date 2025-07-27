using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceExplorer.API.Data;

//[Authorize]
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
        var planets = await _context.Planets.Take(20).ToListAsync(); // only 20 for now
        return Ok(planets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlanetById(int id)
    {
        var planet = await _context.Planets
            .Include(p => p.AIResult)
            .Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.DiscoveryYear,
                p.Mass,
                p.Radius,
                p.OrbitalPeriod,
                p.DistanceFromEarth,
                p.DiscoveryMethod,
                Predictions = new
                {
                    p.AIResult.PlanetType,
                    p.AIResult.Atmosphere,
                    p.AIResult.WaterLikelihood,
                    p.AIResult.BioScore
                }
            })
            .FirstOrDefaultAsync();

        if (planet == null)
            return NotFound();

        return Ok(planet);
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
                    p.AIResult.Atmosphere,
                    p.AIResult.WaterLikelihood,
                    p.AIResult.BioScore
                }
            })
            .ToListAsync();

        return Ok(data);
    }
}