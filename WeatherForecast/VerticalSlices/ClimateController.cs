using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;
using WeatherForecast.WebApi.ExeptionHandling;

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
        private readonly IWeatherForecastGetLocationService _weatherForecastGetLocationService;

        public ClimateController(
            ILogger<ClimateController> logger,
            IDatabaseContext dbContext,
            IValidator<Location> locationValidator,
            IValidator<ClimateRequest> climateRequestValidator,
            IWeatherForecastGetLocationService weatherForecastGetLocationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _locationValidator = locationValidator;
            _climateRequestValidator = climateRequestValidator;
            _weatherForecastGetLocationService = weatherForecastGetLocationService;
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

            var validationResult = await _climateRequestValidator.ValidateAsync(climateRequest);
            if (!validationResult.IsValid)
                return BadRequest_FromValidation(validationResult);

            var location = new Location(climateRequest.Location);
            var climate = new Climate { 
                Location = location, 
                LowTemperature = climateRequest.LowTemperature, 
                HighTemperature = climateRequest.HighTemperature
            };
            _dbContext.AddClimate(climate);
            
            return CreatedAtAction(nameof(GetLocation),new {country = location.Country, city = location.City }, climate);
        }
        
        private IActionResult BadRequest_FromValidation(ValidationResult validationResult)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();
            return BadRequest(errors);
        }

    }
}