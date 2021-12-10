using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;

namespace WeatherForecast.Core.Features.ForecastFeatures
{
    public class GetForecastHandler : IRequestHandler<GetForecast, IEnumerable<ForecastPoint>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ITemperatureRepository _temperatureRepository;
        private readonly INowProvider _nowProvider;
        private readonly IRandomGenerator _randomGenerator;
        private readonly IValidator<GetForecast> _validator;

        public GetForecastHandler(
            ITemperatureRepository temperatureRepository,
            INowProvider nowProvider,
            IRandomGenerator randomGenerator,
            IValidator<GetForecast> validator)
        {
            _temperatureRepository = temperatureRepository;
            _nowProvider = nowProvider;
            _randomGenerator = randomGenerator;
            _validator = validator;
        }
        public Task<IEnumerable<ForecastPoint>> Handle(GetForecast request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var location = new Location(request.Location);

            var temperatureRange = _temperatureRepository.Get(location);
            var startDate = _nowProvider.Now();

            var weatherForecast =  Enumerable.Range(1, request.Days).Select(index => new ForecastPoint
            {
                Date = startDate.AddDays(index),
                TemperatureC = _randomGenerator.GetRange(temperatureRange.Low, temperatureRange.High),
                Summary = Summaries[_randomGenerator.GetRange(0, Summaries.Length)]
            }).AsEnumerable();

            return Task.FromResult(weatherForecast);
        }
    }
}
