using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Model;

namespace WeatherForecast.Repositorie
{
    public interface ITemperatureRepository
    {
        TemperatureRange Get(string str);
    }
}
