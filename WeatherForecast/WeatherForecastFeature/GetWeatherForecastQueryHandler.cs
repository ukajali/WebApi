using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Contracts;
using WeatherForecast.Model;
using WeatherForecast.Repositories;

namespace WeatherForecast.WeatherForecastFeature
{
    public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, IEnumerable<Model.WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ITemperatureRepository _temperatureRepository;
        private readonly INowProvider _nowProvider;
        private readonly IRandomGenerator _randomGenerator;

        public GetWeatherForecastQueryHandler(
            ITemperatureRepository temperatureRepository,
            INowProvider nowProvider,
            IRandomGenerator randomGenerator)
        {
            _temperatureRepository = temperatureRepository;
            _nowProvider = nowProvider;
            _randomGenerator = randomGenerator;
        }
        public Task<IEnumerable<Model.WeatherForecast>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var temperatureRange = _temperatureRepository.Get(request.Location);
            Validate(temperatureRange);

            var startDate = _nowProvider.Now();

            var weatherForecast =  Enumerable.Range(1, request.Days).Select(index => new Model.WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = _randomGenerator.GetRange(temperatureRange.Low, temperatureRange.High),
                Summary = Summaries[_randomGenerator.GetRange(0, Summaries.Length)]
            }).AsEnumerable();

            return Task.FromResult(weatherForecast);
        }
        private void Validate(TemperatureRange temperatureRange)
        {
            if (temperatureRange == null)
                throw new ArgumentException("not found temperature range for specific location");
        }      
    }
}
