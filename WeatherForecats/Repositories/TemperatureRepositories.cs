using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecats.Model;
using WeatherForecats.Repositories.DataBaseInMemory;

namespace WeatherForecats.Repositories
{
    public class TemperatureRepositories: IRepository<TemperatureRange>
    {
        public TemperatureRange Get(string location)
        {
            var climates =
                from climate in DatabaseInMemory.LocationClimates
                where climate.Location == location
                select new TemperatureRange(climate.LowTemperature, climate.HighTemperature);
            // simulate real database response delay and async processing
            Task.Delay(2, CancellationToken.None);
            return climates.FirstOrDefault();
        }
    }
}
