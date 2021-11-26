using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Features.ClimateFeatures;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;
using System.Linq;

namespace WeatherForecast.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClimateController : ControllerBase
    {
        private readonly ILogger<ClimateController> _logger;
        private readonly IMediator _mediator;
        private readonly IValidator<Location> _locationValidator;
        private readonly IValidator<ClimateRequest> _climateRequestValidator;
        public ClimateController(
            ILogger<ClimateController> logger,
            IMediator mediator,
            IValidator<Location> locationValidator,
            IValidator<ClimateRequest> climateRequestValidator)
        {
            _logger = logger;
            _mediator = mediator;
            _locationValidator = locationValidator;
            _climateRequestValidator = climateRequestValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            return Ok(await _mediator.Send(new GetClimates()));
        }

        [HttpGet("{country}/{city}")]
        public async Task<IActionResult> GetLocation(string country,  string city)
        {
            var newLocation = new Location(country, city);
            var validationResult = _locationValidator.Validate(newLocation);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x=>x.ErrorMessage).ToArray());

            var result = await _mediator.Send(new GetClimateForLocation(newLocation));           
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClimateRequest climateRequest)
        {
            _logger.LogInformation(
                "[POST] WeatherForecast. location:{Location} lowTemperature:{LowTemperature} highTemperature:{HighTemperature}"
                , climateRequest.Location, climateRequest.LowTemperature, climateRequest.HighTemperature);

            var validationResult = _climateRequestValidator.Validate(climateRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

            var location = new Location(climateRequest.Location);
            var createClimate = new CreateClimate(location,
                climateRequest.LowTemperature, climateRequest.HighTemperature);

            var result = await _mediator.Send(createClimate);
            return CreatedAtAction(nameof(GetLocation),new {country = location.Country, city = location.City }, result);
        }
    }
}