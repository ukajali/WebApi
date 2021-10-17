using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Model
{
    public abstract class TimeProvider
    {
        private static TimeProvider current = DefaultTimeProvider.Instance;

        public static TimeProvider Current
        {
            get { return current; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                TimeProvider.current = value;
            }
        }

        public abstract DateTime UtcNow { get; }

        public static void ResetToDefault()
        {
            TimeProvider.current = DefaultTimeProvider.Instance;
        }
    }
    public class DefaultTimeProvider : TimeProvider
    {
        private static TimeProvider instance;

        public static TimeProvider Instance
        {
            get { return instance ?? (instance = new DefaultTimeProvider()); }
        }

        private DefaultTimeProvider()
        {

        }

        public override DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}
