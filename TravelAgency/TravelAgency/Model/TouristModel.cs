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
        public TourModel Tour { get; set; }
        public TouristCheck TouristCheck { get; set; }

        public TouristModel(TourModel tour, TouristCheck touristCheck)
        {
            Tour = tour;
            TouristCheck = touristCheck;
        }

        public TouristModel(string username, string password, string name, string surname, string email, Role role, TourModel tour, TouristCheck touristCheck) : base(username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristCheck = touristCheck;
        }

    }
}
