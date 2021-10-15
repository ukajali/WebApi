using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Model;
using WeatherForecast.Repositorie.DataBaseInMemory;

namespace WeatherForecast.Repositorie
{
    public class TemperatureRepositorie: ITemperatureRepositorie
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
