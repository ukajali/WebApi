using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Contracts;
using WeatherForecast.Model;

namespace WeatherForecast.Repositories.DataBaseInMemory
{
    public class MemoryDatabaseContext : IDatabaseContext
    {
        public IQueryable<Climate> LocationClimates => DatabaseInMemory.LocationClimates.AsQueryable();

        public void AddClimate(Climate climate) => DatabaseInMemory.LocationClimates.Add(
            new Climate
            {
                Location = climate.Location,
                LowTemperature = climate.LowTemperature,
                HighTemperature = climate.HighTemperature
            });

        private static class DatabaseInMemory
        {
            internal static readonly List<Climate> LocationClimates = new()
            {
                new Climate { Location = "poland/krakow", LowTemperature = -15, HighTemperature = 38 },
                new Climate { Location = "india/chennai", LowTemperature = -1, HighTemperature = 55 },
                new Climate { Location = "usa/cleveland", LowTemperature = -10, HighTemperature = 42 },
                new Climate { Location = "usa/new-york", LowTemperature = -10, HighTemperature = 42 },
                new Climate { Location = "usa/san-francisco", LowTemperature = -1, HighTemperature = 49 },
                new Climate { Location = "usa/redmond", LowTemperature = -12, HighTemperature = 38 }
            };
        }
    }
}
