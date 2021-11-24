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
            // TODO: splitting location to country and city
            Country = location;
            City = "";
        }
        
        // TODO: implement overloads: Equals, GetHashCode
        /*
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        */

        public override string ToString()
        {
            return Country + Delimiter + City;
        }
    }
}
