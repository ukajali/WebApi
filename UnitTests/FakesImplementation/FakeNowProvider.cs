using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts;

namespace UnitTests.FakesImplementation
{
    public class FakeNowProvider : INowProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
