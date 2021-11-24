using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Features.ClimateFeatures;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;

namespace WeatherForecast.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClimateController : ControllerBase
    {
        private readonly ILogger<ClimateController> _logger;
        private readonly IMediator _mediator;
        public ClimateController(
            ILogger<ClimateController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
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

            // TODO: Inject IValidator<Location> _locationValidator
            // var locValidationResult = _locationValidator.Validate(newLocation);
            // if (!locValidationResult.IsValid)
            //     return BadRequest( /* validateResult.Errors => string[] */ );

            var result = await _mediator.Send(new GetClimateForLocation(newLocation));
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClimateRequest climateRequest)
        {
            _logger.LogInformation(
                "[POST] WeatherForecast. location:{Location} lowTemperature:{LowTemperature} highTemperature:{HighTemperature}"
                , climateRequest.Location, climateRequest.LowTemperature, climateRequest.HighTemperature);

            var location = new Location(climateRequest.Location);

            // TODO: Use FluentValidation for ClimateRequest
            // inject: IValidator<ClimateRequest> _climateRequestValidator
            // var validateResult = _climateRequestValidator.Validate(climateRequest)
            // if (!validateResult.IsValid)
            //     return BadRequest( /* validateResult.Errors => string[] */ );

            var createClimate = new CreateClimate(location, 
                climateRequest.LowTemperature, climateRequest.HighTemperature);
            var result = await _mediator.Send(createClimate);
            return CreatedAtAction(nameof(GetLocation),new {country = location.Country, city = location.City }, result);
        }
    }

    public record ClimateRequest(string Location, int LowTemperature, int HighTemperature);
}