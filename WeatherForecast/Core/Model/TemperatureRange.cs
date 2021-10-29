namespace WeatherForecast.Core.Model
{
    public class TemperatureRange
    {
        public int Low { get; }
        public int High { get; }

        public TemperatureRange(int low, int high)
        {
            Low = low;
            High = high;
        }
    }
}
