using HelloDot.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloDotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherClient client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherClient client)
        {
            _logger = logger;
            this.client = client;
        }

        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast>  Get(string city)
        {

            var forecast = await client.getCurrentWeatherAsync(city);

            return new WeatherForecast
            {
                Summary = forecast.weather[0].description,
                TemperatureC = (int)forecast.main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime

            };
          
        }
    }
}
