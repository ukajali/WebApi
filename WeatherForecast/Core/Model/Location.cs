using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Core.Model
{
    public class Location
    {
        public string Country { get; set; }
        public string City { get; set; }

        private string Delimiter = "/";

        private string FullLocation { get; set; }

        public Location(string country, string city)
        {
            Country = country;
            City = city;
            FullLocation = Country + Delimiter + City;
        }
        public string GetFullLocation()
        {
            return FullLocation;
        }
    }
}
