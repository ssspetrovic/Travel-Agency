namespace TravelAgency.Model
{
    public enum TouristCheck
    {
        Unchecked,
        Pinged,
        Checked
    }
    public class Tourist : User
    {
        public Tour Tour { get; set; } = null!;
        public TouristCheck TouristCheck { get; set; }
        public int LocationId { get; set; }

        public Tourist()
        {

        }

        public Tourist(Tour tour, TouristCheck touristCheck , int locationId)
        {
            Tour = tour;
            TouristCheck = touristCheck;
            LocationId = locationId;
        }

        public Tourist(int id, Tour tour, TouristCheck touristCheck, int locationId)
        {
            Id = id;
            Tour = tour;
            TouristCheck = touristCheck;
            LocationId = locationId;
        }

        public Tourist(string username, string password, string name, string surname, string email, Role role, Tour tour, TouristCheck touristCheck, int locationId) : base(username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristCheck = touristCheck;
            LocationId = locationId;
        }


        public Tourist(int id, string username, string password, string name, string surname, string email, Role role, Tour tour, TouristCheck touristCheck, int locationId) : base(id, username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristCheck = touristCheck;
            LocationId = locationId;
        }

    }
}
