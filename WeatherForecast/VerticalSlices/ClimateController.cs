using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;

namespace WeatherForecast.VerticalSlices
{
    [ApiController]
    [Route("[controller]")]
    public class ClimateController : ControllerBase
    {
        private readonly ILogger<ClimateController> _logger;
        private readonly IDatabaseContext _dbContext;
        private readonly IValidator<Location> _locationValidator;
        private readonly IValidator<ClimateRequest> _climateRequestValidator;

        public ClimateController(
            ILogger<ClimateController> logger,
            IDatabaseContext dbContext,
            IValidator<Location> locationValidator,
            IValidator<ClimateRequest> climateRequestValidator)
        {
            _logger = logger;
            _dbContext = dbContext;
            _locationValidator = locationValidator;
            _climateRequestValidator = climateRequestValidator;
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
            _logger.LogInformation("[GET] Climate {Country}/{City}", country, city);
            
            var newLocation = new Location(country, city);
            await _locationValidator.ValidateAndThrowAsync(newLocation);
            var locationClimate = _dbContext.LocationClimates
                .FirstOrDefault(x => x.Location.City == city && x.Location.Country == country);

            return Ok(locationClimate);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] ClimateRequest climateRequest)
        {
            _logger.LogInformation(
                "[POST] Climate. location:{Location} lowTemperature:{LowTemperature} highTemperature:{HighTemperature}"
                , climateRequest.Location, climateRequest.LowTemperature, climateRequest.HighTemperature);

            await _climateRequestValidator.ValidateAndThrowAsync(climateRequest);
            var location = new Location(climateRequest.Location);
            var climate = new Climate
            {
                Location = location,
                LowTemperature = climateRequest.LowTemperature,
                HighTemperature = climateRequest.HighTemperature
            };
            _dbContext.AddClimate(climate);

            return CreatedAtAction(
                nameof(GetLocation), 
                new { country = climate.Location.Country, city = climate.Location.City }, 
                location);
        }
    }
}