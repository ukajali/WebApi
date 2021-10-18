namespace WeatherForecast.Contracts
{
    public interface IRandomGenerator
    {
        int GetRange(int low, int high);
    }
}