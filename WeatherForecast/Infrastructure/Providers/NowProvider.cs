using System;
using WeatherForecast.Core.Contracts;

namespace WeatherForecast.Infrastructure.Providers
{
    public class NowProvider : INowProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}
