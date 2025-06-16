using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NasaController : ControllerBase
{
    private readonly NasaDataService _service;

    public NasaController(NasaDataService service)
    {
        _service = service;
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncData()
    {
        await _service.FetchAndStorePlanetsAsync();
        return Ok("Planet data fetched and stored.");
    }
}