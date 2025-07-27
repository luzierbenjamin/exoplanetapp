using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace SpaceExplorer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MLController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public MLController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("predict-type")]
        public async Task<IActionResult> PredictPlanetType([FromBody] PlanetData planet)
        {
            var payload = new StringContent(
                JsonSerializer.Serialize(planet),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("http://localhost:5001/predict", payload);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Prediction failed");

            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");

        }
    }

    public class PlanetData
    {
        public double Mass { get; set; }
        public double Radius { get; set; }
        public double OrbitalPeriod { get; set; }
    }
}