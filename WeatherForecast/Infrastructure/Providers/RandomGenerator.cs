using System;
using WeatherForecast.Core.Contracts;

namespace WeatherForecast.Infrastructure.Providers
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
