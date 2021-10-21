using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WeatherForecast.Dto;
using WeatherForecast.WeatherForecastFeature;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int days, string location)
        {
            _logger.LogInformation("[GET] WeatherForecast. days:{days} location:{location}", days, location);
            return Ok(_mapper.Map<List<WeatherForecastDto>>(await _mediator.Send(new GetWeatherForecastQuery(days, location))));         
        }
    }
}
