using System;

namespace WeatherForecast.Core.Model

{
    public class ForecastPoint
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }
}
