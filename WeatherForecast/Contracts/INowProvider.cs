using System;

namespace WeatherForecast.Contracts
{
    public interface INowProvider
    {
        DateTime Now();
    }
}