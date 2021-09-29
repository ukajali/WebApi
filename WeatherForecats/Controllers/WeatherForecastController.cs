using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecats.WeatherForecastFeature;
using AutoMapper;
using WeatherForecats.Dto;

namespace WeatherForecats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IMediator _mediator;
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
            return Ok(_mapper.Map<List<WeatherForecastDto>>(await _mediator.Send(new GetWeatherForecastQuery(days, location))));         
        }
    }
}
