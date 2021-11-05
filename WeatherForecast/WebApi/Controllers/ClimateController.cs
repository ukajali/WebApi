using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Features.ClimateFeatures;

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
        public async Task<IActionResult> GetLocation(string country, string city)
        {
            var location = $"{country}/{city}";
            var result = await _mediator.Send(new GetClimateForLocation(location));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string location, int lowTemperature, int highTemperature)
        {
            _logger.LogInformation(
                "[POST] WeatherForecast. location:{location} lowTemperature:{lowTemperature} highTemperature:{lowTemperature}"
                , location, lowTemperature, highTemperature);

            if(string.IsNullOrEmpty(location))
            {
                return BadRequest("Location value is not provided");
            }

            if(lowTemperature >= highTemperature)
            {
                return BadRequest("lowTemperature cannot be tha same or higher than highTemperature");
            }
            var splitedLocation = location.Split('/');
            var result = (await _mediator.Send(new CreateClimate(location, lowTemperature, highTemperature)));
            return CreatedAtAction(nameof(GetLocation),new {country = splitedLocation[0], city= splitedLocation[1] }, result);
        }
    }
}