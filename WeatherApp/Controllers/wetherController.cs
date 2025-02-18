using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeteoGaliciaApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BASE_URL = "https://servizos.meteogalicia.gal/apiv4";

        public WeatherController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["MeteoGalicia:ApiKey"];
        }

        public async Task<IActionResult> Index()
        {
            return View("Index");
        }

        public async Task<IActionResult> GetWeatherData()
        {
            string coords = "-8.32,43.36"; // Ejemplo: Santiago de Compostela
            string url = $"{BASE_URL}/getNumericForecastInfo?coords={coords}&variables=temperature,wind&API_KEY={_apiKey}";
            
            var response = await _httpClient.GetStringAsync(url);
            return Content(response, "application/json");
        }
    }
}
