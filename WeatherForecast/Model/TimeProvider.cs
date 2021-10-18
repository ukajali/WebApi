using System;

namespace WeatherForecast.Model
{
    public abstract class TimeProvider
    {
        private static TimeProvider _current = DefaultTimeProvider.Instance;

        public static TimeProvider Current
        {
            get => _current;
            set => _current = value ?? throw new ArgumentNullException(nameof(value));
        }

        public abstract DateTime UtcNow { get; }

        public static void ResetToDefault()
        {
            _current = DefaultTimeProvider.Instance;
        }
    }
    public class DefaultTimeProvider : TimeProvider
    {
        private static TimeProvider _instance;

        public static TimeProvider Instance => _instance ??= new DefaultTimeProvider();

        private DefaultTimeProvider() { }

        public override DateTime UtcNow => DateTime.UtcNow;
    }
}
