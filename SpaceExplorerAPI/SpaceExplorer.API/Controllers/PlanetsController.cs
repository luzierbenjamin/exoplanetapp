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
}