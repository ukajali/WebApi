using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecats.Dto;

namespace WeatherForecats.WeatherForecastFeature
{
    public class GetWeatherForecastQuery : IRequest<IEnumerable<WeatherForecastDto>>
    {
        public int Days { get; }
        public string Location { get; }

        public GetWeatherForecastQuery(int? days, string location)
        {
            ValidateDays(days);
            ValidateLocation(location);
            Days = days.Value;
            Location = location;
        }

        private static void ValidateDays(int? days)
        {
            if (days is null)
                throw new ArgumentNullException(nameof(days));
            if (days is < 1 or > 14)
                throw new ArgumentException("Expected value between 1..14", nameof(days));
        }
        private static void ValidateLocation(string location)
        { 
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentNullException(nameof(location));
        }
    }
}
