using System.Collections.Generic;
using MediatR;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Features.ForecastFeatures
{
    public class GetForecast : IRequest<IEnumerable<ForecastPoint>>
    {
        public string Location { get; }
        public int Days { get; }

        public GetForecast(string location, int? days)
        {
            Days = days.GetValueOrDefault();
            Location = location;
        }
    }
}
