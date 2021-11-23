using System;
using System.Collections.Generic;
using MediatR;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Features.ForecastFeatures
{
    public class GetForecast : IRequest<IEnumerable<ForecastPoint>>
    {
        public int Days { get; }
        public Location Location { get; }

        public GetForecast(int? days, Location location)
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
        private static Location ValidateLocation(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.GetFullLocation()))
                throw new ArgumentNullException(nameof(location));
            else return location;
        }
    }
}
