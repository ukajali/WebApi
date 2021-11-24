using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Features.ForecastFeatures;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;
using WeatherForecast.WebApi.Responses;

namespace WeatherForecast.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForecastController : ControllerBase
    {
        private readonly ILogger<ForecastController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ForecastController(
            ILogger<ForecastController> logger, 
            IMediator mediator, 
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string location, int days)
        {
            _logger.LogInformation("[GET] WeatherForecast. days:{Days} location:{Location}", days, location);

            var getLocation = new Location(location);

            return Ok(_mapper.Map<List<WeatherForecastResponse>>(
                await _mediator.Send(new GetForecast(days, getLocation))));
        }
    }
}
