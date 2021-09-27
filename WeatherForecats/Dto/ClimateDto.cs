using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecats.Dto
{
    // Data transfer object
    public class ClimateDto
    {
        public string Location { get; set; }
        public int LowTemperature { get; set; }
        public int HighTemperature { get; set; }
    }
}
