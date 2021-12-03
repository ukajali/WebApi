using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<GetForecast> _getForecastValidator;

        public ForecastController(
            ILogger<ForecastController> logger, 
            IMediator mediator, 
            IMapper mapper,
            IValidator<GetForecast> getForecastValidator)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _getForecastValidator = getForecastValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string location, int days)
        {
            _logger.LogInformation("[GET] WeatherForecast. days:{Days} location:{Location}", days, location);

            var getForecast = new GetForecast(location,days);

            var response = await _mediator.Send(getForecast);

            return Ok(_mapper.Map<List<WeatherForecastResponse>>(response));
        }
    }
}
