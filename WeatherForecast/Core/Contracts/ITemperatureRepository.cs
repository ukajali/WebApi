using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Contracts
{
    public interface ITemperatureRepository
    {
        TemperatureRange Get(Location str);
    }
}
