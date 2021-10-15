using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Model;
using WeatherForecast.Repositorie;
using WeatherForecast.Dto;

namespace WeatherForecast.WeatherForecastFeature
{
    public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, IEnumerable<Model.WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ITemperatureRepository _temperatureRepository;

        public GetWeatherForecastQueryHandler(ITemperatureRepository temperatureRepository)
        {
            _temperatureRepository = temperatureRepository;
        }
        public Task<IEnumerable<Model.WeatherForecast>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var rng = new Random();
            var temperatureRange = _temperatureRepository.Get(request.Location);
            Validate(temperatureRange);
            var weatherForecast =  Enumerable.Range(1, request.Days).Select(index => new Model.WeatherForecast
            {
                Date = request.Date.AddDays(index),
                TemperatureC = rng.Next(temperatureRange.Low, temperatureRange.High),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).AsEnumerable();

            return Task.FromResult(weatherForecast);
        }
        private void Validate(TemperatureRange temperatureRange)
        {
            if (temperatureRange == null)
                throw new ArgumentException("not found temerature range for specific location");
        }      
    }
}
