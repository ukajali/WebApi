using MediatR;
using System;
using System.Collections.Generic;
using WeatherForecast.Model;

namespace WeatherForecast.WeatherForecastFeature
{
    public class AddClimateDatabaseInMemoryQuery : IRequest<Climate>
    {

        public string Location { get; set; }
        public int LowTemperature { get; set; }
        public int HighTemperature { get; set; }

        public AddClimateDatabaseInMemoryQuery(string location, int lowTemperature, int highTemperature )
        {
            Location = ValidateLocation(location);
            LowTemperature = lowTemperature;
            HighTemperature = highTemperature;
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
