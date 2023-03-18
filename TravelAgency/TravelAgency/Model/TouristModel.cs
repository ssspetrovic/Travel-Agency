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

        public TouristModel()
        {

        }

        public TouristModel(TourModel tour, TouristCheck touristCheck)
        {
            Tour = tour;
            TouristCheck = touristCheck;
        }

        public TouristModel(int id, TourModel tour, TouristCheck touristCheck)
        {
            Id = id;
            Tour = tour;
            TouristCheck = touristCheck;
        }

        public TouristModel(string username, string password, string name, string surname, string email, Role role, TourModel tour, TouristCheck touristCheck) : base(username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristCheck = touristCheck;
        }


        public TouristModel(int id, string username, string password, string name, string surname, string email, Role role, TourModel tour, TouristCheck touristCheck) : base(username, password, name, surname, email, role)
        {
            Id = id;
            Tour = tour;
            TouristCheck = touristCheck;
        }

    }
}
