using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Model
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
