using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Model
{
    public class Climate
    {
        public string Location { get; set; }
        public int LowTemperature { get; set; }
        public int HighTemperature { get; set; }
    }
}
