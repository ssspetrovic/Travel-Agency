namespace TravelAgency.Model
{
    public enum TouristAppearance
    {
        Unknown,
        Pinged,
        Present,
        Absent
    }
    public class Tourist : User
    {
        public Tour Tour { get; set; } = null!;
        public TouristAppearance TouristAppearance { get; set; }
        public int LocationId { get; set; }

        public Tourist()
        {

        }

        public Tourist(Tour tour, TouristAppearance touristAppearance , int locationId)
        {
            Tour = tour;
            TouristAppearance = touristAppearance;
            LocationId = locationId;
        }

        public Tourist(int id, Tour tour, TouristAppearance touristAppearance, int locationId)
        {
            Id = id;
            Tour = tour;
            TouristAppearance = touristAppearance;
            LocationId = locationId;
        }

        public Tourist(string username, string password, string name, string surname, string email, Role role, Tour tour, TouristAppearance touristAppearance, int locationId) : base(username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristAppearance = touristAppearance;
            LocationId = locationId;
        }


        public Tourist(int id, string username, string password, string name, string surname, string email, Role role, Tour tour, TouristAppearance touristAppearance, int locationId) : base(id, username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristAppearance = touristAppearance;
            LocationId = locationId;
        }

    }
}
