using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Model;

namespace WeatherForecast.WeatherForecastFeature
{
    public class GetWeatherForecastQuery : IRequest<IEnumerable<Model.WeatherForecast>>
    {
        public int Days { get; }
        public string Location { get; }

        public GetWeatherForecastQuery(int? days, string location)
        {
            Days = ValidateDays(days);
            Location = ValidateLocation(location);
        }
        
        private static int ValidateDays(int? days)
        {
            if (days is null)
                throw new ArgumentNullException(nameof(days));
            else if (days is < 1 or > 14)
                throw new ArgumentException("Expected value between 1..14", nameof(days));
            else return days.Value;
        }
        private static string ValidateLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentNullException(nameof(location));
            else return location;
        }
    }
}
