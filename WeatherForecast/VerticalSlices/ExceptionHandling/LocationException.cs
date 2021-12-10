using System;

namespace WeatherForecast.VerticalSlices.ExceptionHandling
{
    public class LocationException : SystemException
    {
        public LocationException(string message) : base(message)
        {
        }
    }
    public class LocationErrorException : LocationException
    {
        public LocationErrorException(string message) : base(message)
        {
        }
    }
    public class ClimateException : SystemException
    {
        public ClimateException(string message) : base(message)
        {
        }
    }
    public class ClimateErrorException : ClimateException
    {
        public ClimateErrorException(string message) : base(message)
        {
        }
    }
}
