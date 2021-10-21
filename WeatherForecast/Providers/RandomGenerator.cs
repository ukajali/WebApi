using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Contracts;

namespace WeatherForecast.Providers
{
    public class RandomGenerator : IRandomGenerator
    {
        public int GetRange(int low, int high)
        {
            var rng = new Random();
            return rng.Next(low, high);
        }
    }
}
