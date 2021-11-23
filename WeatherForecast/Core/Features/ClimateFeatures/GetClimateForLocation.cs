using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Features.ClimateFeatures
{
    public class GetClimateForLocation : IRequest<Climate>
    {
        public Location Location { get; set; }
        public GetClimateForLocation(Location location)
        {
            Location = location;
        }
    }
}
