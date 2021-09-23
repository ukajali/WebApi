using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecats.WeatherForecastFeature
{
    public class GetWeatherForecastQuery : IRequest<IEnumerable<WeatherForecast>>
    {
        public int Day { get; }
        
        public GetWeatherForecastQuery(int? day)
        {
            if (day is null)
                throw new ArgumentNullException(nameof(day));
            if (day is < 1 or > 14)
                throw new ArgumentException("Expected value between 1..14",nameof(day));
            Day = day.Value;
        }
    }
}
