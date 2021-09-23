using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecats.WeatherForecastFeature
{
    public class GetWeatherForecastQuery : IRequest<IEnumerable<WeatherForecast>>
    {
        public int Day;
        public GetWeatherForecastQuery(int day)
        {
            Day = day;
        }
    }
}
