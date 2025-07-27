using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace SpaceExplorer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PredictionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public class CustomPredictionInput
        {
            public double Mass { get; set; }
            public double Radius { get; set; }
            public double OrbitalPeriod { get; set; }
        }

        [HttpPost("custom")]
        public async Task<IActionResult> PredictCustom([FromBody] CustomPredictionInput input)
        {
            if (input.Mass <= 0 || input.Radius <= 0 || input.OrbitalPeriod <= 0)
                return BadRequest("All input values must be greater than 0.");

            try
            {
                var client = _httpClientFactory.CreateClient();

                // 1. Predict Planet Type
                var planetType = await PostToModel<string>(client, "/predict-type", new
                {
                    mass = input.Mass,
                    radius = input.Radius,
                    orbitalPeriod = input.OrbitalPeriod
                });

                // 2. Predict Atmosphere
                var atmosphere = await PostToModel<string>(client, "/predict-atmosphere", new
                {
                    mass = input.Mass,
                    radius = input.Radius,
                    orbitalPeriod = input.OrbitalPeriod,
                    planetType
                });

                // 3. Predict Water Likelihood
                var waterLikelihood = await PostToModel<double>(client, "/predict-water", new
                {
                    mass = input.Mass,
                    radius = input.Radius,
                    orbitalPeriod = input.OrbitalPeriod,
                    planetType,
                    atmosphere
                });

                // 4. Predict BioScore
                var bioScore = await PostToModel<double>(client, "/predict-bioscore", new
                {
                    mass = input.Mass,
                    radius = input.Radius,
                    orbitalPeriod = input.OrbitalPeriod,
                    planetType,
                    atmosphere,
                    waterLikelihood
                });

                // Return combined prediction
                return Ok(new
                {
                    PlanetType = planetType,
                    Atmosphere = atmosphere,
                    WaterLikelihood = waterLikelihood,
                    BioScore = bioScore
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Prediction failed: {ex.Message}");
            }
        }

        private async Task<T> PostToModel<T>(HttpClient client, string path, object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"http://localhost:5001{path}", content);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var doc = await JsonDocument.ParseAsync(stream);
            var root = doc.RootElement;

            if (typeof(T) == typeof(string))
                return (T)(object)root.EnumerateObject().First().Value.GetString();
            if (typeof(T) == typeof(double))
                return (T)(object)root.EnumerateObject().First().Value.GetDouble();

            throw new InvalidOperationException("Unsupported return type.");
        }
    }
}
