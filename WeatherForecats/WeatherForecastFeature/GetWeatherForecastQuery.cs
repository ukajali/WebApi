using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecats.WeatherForecastFeature
{
    public class GetWeatherForecastQuery : IRequest<IEnumerable<WeatherForecast>>
    {
        public int Days { get; }
        
        public GetWeatherForecastQuery(int? days)
        {
            if (days is null)
                throw new ArgumentNullException(nameof(days));
            if (days is < 1 or > 14)
                throw new ArgumentException("Expected value between 1..14",nameof(days));
            Days = days.Value;
        }
    }
}
