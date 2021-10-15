using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Model;

namespace WeatherForecast.WeatherForecastFeature
{
    public class GetWeatherForecastQuery : IRequest<IEnumerable<Model.WeatherForecast>>
    {
        public int Days { get; }
        public string Location { get; }
        public DateTime Date { get; }

        public GetWeatherForecastQuery(int? days, string location, string date)
        {
            Days = ValidateDays(days);
            Location = ValidateLocation(location);
            Date = ValidateDate(date);
        }
        private static DateTime ValidateDate(string date)
        {
            DateTime currentDate;
            if (!DateTime.TryParse(date, out currentDate))
                throw new ArgumentException("Wrong date, Expected value example: YY-MM-DD => 2021-01-31", nameof(date));
            else if(currentDate < DateTime.Today)
                throw new ArgumentException("Wrong date, Date shouldn't be lover that today's date", nameof(date));
            else return currentDate;
        }

        private static int ValidateDays(int? days)
        {
            if (days is null)
                throw new ArgumentNullException(nameof(days));
            else if (days is < 1 or > 14)
                throw new ArgumentException("Expected value between 1..14", nameof(days));
            else return days.Value;
        }
        private static string ValidateLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentNullException(nameof(location));
            else return location;
        }
    }
}
