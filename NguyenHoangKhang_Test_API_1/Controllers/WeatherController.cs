using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NguyenHoangKhang_Test_API.Models;

namespace NguyenHoangKhang_Test_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(IMapper mapper, ILogger<WeatherController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather()
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiUrl = "http://api.openweathermap.org/data/2.5/group?id=1580578,1581129,1581297,1581188,1587%20923&units=metric&appid=91b7466cc755db1a94caf6d86a9c788a";
                WeatherAPIResult weatherResult = await client.GetFromJsonAsync<WeatherAPIResult>(apiUrl);
                List<WeatherData> weatherData = _mapper.Map<List<WeatherData>>(weatherResult.list);
                WeatherResponse weatherResponse = new WeatherResponse
                {
                    data = weatherData,
                    message = "Current weather information of cities",
                    statusCode = 200
                };
                return Ok(weatherResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
    }
}
