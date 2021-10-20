using System;
using WeatherForecast.Contracts;

namespace UnitTests.Fakes
{
    public class FakeRandomGenerator : IRandomGenerator
    {
        private readonly Random _random;

        public FakeRandomGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public int GetRange(int low, int high) => _random.Next(low, high);
    }
}
