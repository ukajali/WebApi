using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Core.Model
{
    public record ClimateRequest(string Location, int LowTemperature, int HighTemperature);
}
