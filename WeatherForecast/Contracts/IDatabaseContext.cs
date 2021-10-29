using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Model;

namespace WeatherForecast.Contracts
{
    public interface IDatabaseContext
    {
        IQueryable<Climate> LocationClimates { get; }
        void AddClimate(Climate climate);
    }
}
