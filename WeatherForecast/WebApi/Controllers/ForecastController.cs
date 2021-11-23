using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Core.Features.ForecastFeatures;
using WeatherForecast.Core.Model;
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
        public async Task<IActionResult> Get(int days, string country, string city)
        {
            var newLocation = new Location(country, city);

            _logger.LogInformation("[GET] WeatherForecast. days:{days} location:{location}", days, newLocation);

            var daysValidator = (IValidator<int>)HttpContext.RequestServices.GetService(typeof(IValidator<int>));
            var daysValidatorResult = daysValidator.Validate(days);
            if (!daysValidatorResult.IsValid)
                return BadRequest($"Location value is not provided: {string.Join(",", daysValidatorResult.Errors)}");

            
            var locationValidator = (IValidator<Location>)HttpContext.RequestServices.GetService(typeof(IValidator<Location>));
            var locValidationResult = locationValidator.Validate(newLocation);
            if (!locValidationResult.IsValid)
                return BadRequest($"Location value is not provided: {string.Join(",", locValidationResult.Errors)}");

            return Ok(_mapper.Map<List<WeatherForecastResponse>>(
                await _mediator.Send(new GetForecast(days, newLocation))));
        }
    }
}
