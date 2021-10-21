using WeatherForecast.Model;

namespace WeatherForecast.Repositories
{
    public interface ITemperatureRepository
    {
        TemperatureRange Get(string str);
    }
}
