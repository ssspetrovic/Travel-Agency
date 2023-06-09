
using System;
using System.Text;

namespace TravelAgency.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }

        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{City}, {Country}");
            return sb.ToString();
        }
    }
}
