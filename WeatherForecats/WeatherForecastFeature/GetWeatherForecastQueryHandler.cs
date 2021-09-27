using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecats.Dto;
using WeatherForecats.Model;
using WeatherForecats.Services;

namespace WeatherForecats.WeatherForecastFeature
{
    public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, IEnumerable<WeatherForecastDto>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ITemperatureServices _temperatureServices;

        public GetWeatherForecastQueryHandler(ITemperatureServices temperatureServices)
        {
            _temperatureServices = temperatureServices;
        }
        public Task<IEnumerable<WeatherForecastDto>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var rng = new Random();
            var temperatureRange = _temperatureServices.GetTemperatures(request.Location);
            Validate(temperatureRange);
            var weatherForecastResult = Enumerable.Range(1, request.Days).Select(index => new WeatherForecastDto
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(temperatureRange.Low, temperatureRange.High),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
           .AsEnumerable();

            return Task.FromResult(weatherForecastResult);
        }
        private void Validate(TemperatureRange temperatureRange)
        {
            if (temperatureRange == null)
                throw new ArgumentException("not found temerature range for specific location");
        }
    }
}
