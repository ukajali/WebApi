namespace WeatherForecast.Core.Contracts
{
    public interface IRandomGenerator
    {
        int GetRange(int low, int high);
    }
}