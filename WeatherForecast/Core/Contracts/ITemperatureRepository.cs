using WeatherForecast.Core.Model;

namespace WeatherForecast.Core.Contracts
{
    public interface ITemperatureRepository
    {
        TemperatureRange Get(string str);
    }
}
