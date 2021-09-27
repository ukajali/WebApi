using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecats.Dto;

namespace WeatherForecats.DataBase
{
    public interface IDatabaseInMemory
    {
        public ClimateDto[] LocationClimates { get; }
    }
}
