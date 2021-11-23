using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Features.ClimateFeatures;
using WeatherForecast.Core.Model;

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
            var result = await _mediator.Send(new GetClimateForLocation(new Location(country, city)));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string country, string city, int lowTemperature, int highTemperature)
        {
            var newLocation = new Location(country, city);
            _logger.LogInformation(
                "[POST] WeatherForecast. location:{location} lowTemperature:{lowTemperature} highTemperature:{lowTemperature}"
                , newLocation, lowTemperature, highTemperature);
            if (string.IsNullOrEmpty(newLocation.City))
            {
                return BadRequest("Location value is not provided");
            }
            if (lowTemperature >= highTemperature)
            {
                return BadRequest("lowTemperature cannot be tha same or higher than highTemperature");
            }
           // var splitedLocation = location.Split('/');
            var result = (await _mediator.Send(new CreateClimate(newLocation, lowTemperature, highTemperature)));
            return CreatedAtAction(nameof(GetLocation),new {country = country, city = city }, result);
        }
    }
}