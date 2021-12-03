using System;

namespace WeatherForecast.Core.Model.ValueObjects
{
    public class Location
    {
        public string Country { get; }
        public string City { get; }

        private const string Delimiter = "/";

        public Location(string country, string city)
        {
            Country = country;
            City = city;
        }

        public Location(string location)
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentException("Invalid empty location", nameof(location));
            var split = location.Split(Delimiter);
            if (split.Length != 2)
                throw new ArgumentException($"Location requires format: 'country{Delimiter}city'", nameof(location));
            Country = split[0];
            City = split[1];
        }

        private bool Equals(Location other)
        {
            return Country == other.Country && City == other.City;
        }

        public override bool Equals(object obj)
        {
            return obj is not null && obj == this && obj is Location loc && Equals(loc);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Country, City);
        }

        public override string ToString()
        {
            return $"{Country}{Delimiter}{City}";
        }    
    }
}
