
namespace TravelAgency.Model
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; } 
        

        public LocationModel(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }
    }
}
