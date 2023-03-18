namespace TravelAgency.Model
{
    public enum TouristCheck
    {
        Unchecked,
        Pinged,
        Checked
    }
    public class TouristModel : UserModel
    {
        public TourModel Tour { get; set; } = null!;
        public TouristCheck TouristCheck { get; set; }
        public int LocationId { get; set; }

        public TouristModel()
        {

        }

        public TouristModel(TourModel tour, TouristCheck touristCheck , int locationId)
        {
            Tour = tour;
            TouristCheck = touristCheck;
            LocationId = locationId;
        }

        public TouristModel(int id, TourModel tour, TouristCheck touristCheck, int locationId)
        {
            Id = id;
            Tour = tour;
            TouristCheck = touristCheck;
            LocationId = locationId;
        }

        public TouristModel(string username, string password, string name, string surname, string email, Role role, TourModel tour, TouristCheck touristCheck, int locationId) : base(username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristCheck = touristCheck;
            LocationId = locationId;
        }


        public TouristModel(int id, string username, string password, string name, string surname, string email, Role role, TourModel tour, TouristCheck touristCheck, int locationId) : base(id, username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristCheck = touristCheck;
            LocationId = locationId;
        }

    }
}
