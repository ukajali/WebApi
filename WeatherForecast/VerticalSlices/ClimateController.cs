using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;
using WeatherForecast.VerticalSlices.ExeptionHandling;
using WeatherForecast.WebApi.ExeptionHandling;

namespace WeatherForecast.VerticalSlices
{
    [ApiController]
    [Route("[controller]")]
    public class ClimateController : ControllerBase
    {
        private readonly ILogger<ClimateController> _logger;
        private readonly IDatabaseContext _dbContext;
        private readonly IWeatherForecastGetLocationService _weatherForecastGetLocationService;
        private readonly IWeatherForecastCreateLocationService _weatherForecastCreateLocationService;

        public ClimateController(
            ILogger<ClimateController> logger,
            IDatabaseContext dbContext,
            IWeatherForecastGetLocationService weatherForecastGetLocationService,
            IWeatherForecastCreateLocationService weatherForecastCreateLocationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _weatherForecastGetLocationService = weatherForecastGetLocationService;
            _weatherForecastCreateLocationService = weatherForecastCreateLocationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var locations = _dbContext.LocationClimates.AsEnumerable();
            await Task.Delay(1);
            return Ok(locations);
        }

        [HttpGet("{country}/{city}")]
        public async Task<IActionResult> GetLocation(string country,  string city)
        {
            var result = await _weatherForecastGetLocationService.Get(country, city);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] ClimateRequest climateRequest)
        {
            _logger.LogInformation(
                "[POST] WeatherForecast. location:{Location} lowTemperature:{LowTemperature} highTemperature:{HighTemperature}"
                , climateRequest.Location, climateRequest.LowTemperature, climateRequest.HighTemperature);

            var result = await _weatherForecastCreateLocationService.PostLocation(climateRequest);
            var location = result.Location;
            return CreatedAtAction(nameof(GetLocation), new { country = location.Country, city = location.City }, result);
        }
    }
}