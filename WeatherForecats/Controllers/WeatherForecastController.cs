using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecats.WeatherForecastFeature;

namespace WeatherForecats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private IMediator _mediator;

        public WeatherForecastController(
            IMediator mediator,
            ILogger<WeatherForecastController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? day)
        {
            return Ok(await _mediator.Send(new GetWeatherForecastQuery(day)));
        }
    }
}
