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
            var splitedLocation = location.SmartStringLocationSplit();
            Country = splitedLocation.Item1;
            City = splitedLocation.Item2;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;      
            
            if(obj is Location loc)
                return City.Equals(loc.City) && Country.Equals(loc.Country);
            
            return false;         
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Country, City);
        }

        public override string ToString()
        {
            return Country + Delimiter + City;
        }    
    }
}
