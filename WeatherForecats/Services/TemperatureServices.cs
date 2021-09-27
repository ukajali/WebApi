using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecats.DataBase;
using WeatherForecats.Model;

namespace WeatherForecats.Services
{
    public class TemperatureServices : ITemperatureServices
    {
        private readonly IDatabaseInMemory _databaseInMemory;

        public TemperatureServices(IDatabaseInMemory databaseInMemory)
        {
            _databaseInMemory = databaseInMemory;
        }
        public TemperatureRange GetTemperatures(string location)
        {
            var climates =
                from climateDto in _databaseInMemory.LocationClimates
                where climateDto.Location == location
                select new TemperatureRange(climateDto.LowTemperature, climateDto.HighTemperature);
            // simulate real database response delay and async processing
            Task.Delay(2, CancellationToken.None);
            return climates.FirstOrDefault();
        }
    }
}
