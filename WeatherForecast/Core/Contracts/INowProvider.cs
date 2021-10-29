using System;

namespace WeatherForecast.Core.Contracts
{
    public interface INowProvider
    {
        DateTime Now();
    }
}