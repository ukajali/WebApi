using System;
using MediatR;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Features.ClimateFeatures
{
    public class CreateClimate : IRequest<Climate>
    {
        public string Location { get; set; }
        public int LowTemperature { get; set; }
        public int HighTemperature { get; set; }

        public CreateClimate(string location, int lowTemperature, int highTemperature )
        {
            Location = ValidateLocation(location);
            LowTemperature = lowTemperature;
            HighTemperature = highTemperature;
        }

        private static string ValidateLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentNullException(nameof(location));
            return location;
        }
    }
}