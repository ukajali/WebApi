using System;
using System.Collections.Generic;
using MediatR;
using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;

namespace WeatherForecast.Core.Features.ForecastFeatures
{
    public class GetForecast : IRequest<IEnumerable<ForecastPoint>>
    {
        public int Days { get; }
        public Location Location { get; }

        public GetForecast(int? days, Location location)
        {
            Days = days.Value;
            Location = location;
        }
    }
}
