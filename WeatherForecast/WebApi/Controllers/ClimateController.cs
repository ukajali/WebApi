using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Features.ClimateFeatures;

namespace WeatherForecast.Controllers
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

        [HttpPost]
        public async Task<IActionResult> Post(string location, int lowTemperature, int highTemperature)
        {
            _logger.LogInformation(
                "[POST] WeatherForecast. location:{location} lowTemperature:{lowTemperature} highTemperature:{lowTemperature}"
                , location, lowTemperature, highTemperature);
            return 
                Ok (await _mediator.Send(new CreateClimate(location, lowTemperature, highTemperature)));
        }
    }
}