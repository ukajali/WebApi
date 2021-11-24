using System;

namespace WeatherForecast.Core.Model.ValueObjects
{
    public record TemperatureRange()
    {
        public int Low { get; }
        public int High { get; }

        public TemperatureRange(int low, int high) : this()
        {
            Low = low;
            High = high;
            if (low > high)
                throw new Exception("Low temperature is greater than high. Low: {low}, High:{high}");
            if (low is <= -150 or >= 100)
                throw new ArgumentException("Low temperature is out of range (-150 .. 100)", nameof(low));
            if (high is <= -150 or >= 100)
                throw new ArgumentException("High temperature is out of range (-150 .. 100)", nameof(high));
        }

    }
}
