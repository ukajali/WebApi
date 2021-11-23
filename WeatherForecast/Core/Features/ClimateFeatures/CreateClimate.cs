using System;
using MediatR;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Features.ClimateFeatures
{
    public class CreateClimate : IRequest<Climate>
    {
        public Location Location { get; set; }
        public int LowTemperature { get; set; }
        public int HighTemperature { get; set; }

        public CreateClimate(Location location, int lowTemperature, int highTemperature )
        {
            Location = location;
            LowTemperature = lowTemperature;
            HighTemperature = highTemperature;
        }
    }
}