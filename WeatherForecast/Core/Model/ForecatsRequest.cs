using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Core.Model
{
    public record ForecatsRequest(string location, int days);
}
