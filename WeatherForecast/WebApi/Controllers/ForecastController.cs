using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Features.ForecastFeatures;
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
        public async Task<IActionResult> Get(int days, string location)
        {
            _logger.LogInformation("[GET] WeatherForecast. days:{days} location:{location}", days, location);
            return Ok(_mapper.Map<List<WeatherForecastResponse>>(
                await _mediator.Send(new GetForecast(days, location))));

        }
    }
}
