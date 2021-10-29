using System.Collections.Generic;
using System.Linq;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;

namespace WeatherForecast.Infrastructure.Repositories.DataBaseInMemory
{
    public class MemoryDatabaseContext : IDatabaseContext
    {
        private readonly IList<Climate> _locationClimates = new List<Climate>(DatabaseInMemory.LocationClimates);

        public IQueryable<Climate> LocationClimates => _locationClimates.AsQueryable();

        public void AddClimate(Climate climate) => _locationClimates.Add(
            new Climate
            {
                Location = climate.Location,
                LowTemperature = climate.LowTemperature,
                HighTemperature = climate.HighTemperature
            });

        private static class DatabaseInMemory
        {
            internal static readonly IReadOnlyList<Climate> LocationClimates = new List<Climate>
            {
                new() { Location = "poland/krakow", LowTemperature = -15, HighTemperature = 38 },
                new() { Location = "india/chennai", LowTemperature = -1, HighTemperature = 55 },
                new() { Location = "usa/cleveland", LowTemperature = -10, HighTemperature = 42 },
                new() { Location = "usa/new-york", LowTemperature = -10, HighTemperature = 42 },
                new() { Location = "usa/san-francisco", LowTemperature = -1, HighTemperature = 49 },
                new() { Location = "usa/redmond", LowTemperature = -12, HighTemperature = 38 }
            };
        }
    }
}
