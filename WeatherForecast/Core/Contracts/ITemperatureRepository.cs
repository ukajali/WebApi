using WeatherForecast.Core.Model;
using WeatherForecast.Core.Model.ValueObjects;

namespace WeatherForecast.Core.Contracts
{
    public interface ITemperatureRepository
    {
        TemperatureRange Get(Location str);
    }
}
