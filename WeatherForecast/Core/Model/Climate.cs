namespace WeatherForecast.Core.Model
{
    public class Climate
    {
        public Location Location { get; set; }
        public int LowTemperature { get; set; }
        public int HighTemperature { get; set; }
    }
}
