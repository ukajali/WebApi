using WeatherForecats.Model;

namespace WeatherForecats.Services
{
    public interface ITemperatureServices
    {
        TemperatureRange GetTemperatures(string location);
    }
}