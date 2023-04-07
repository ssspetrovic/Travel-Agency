namespace TravelAgency.Model
{
    public enum TouristAppearance
    {
        Unknown,
        Pinged,
        Present,
        Absent
    }

    public enum TouristVoucher
    {
        None,
        GuideSpecific,
        AllTours
    }

    public class Tourist : User
    {
        public Tour Tour { get; set; } = null!;
        public TouristAppearance TouristAppearance { get; set; }
        public int LocationId { get; set; }
        public int Age { get; set; }
        public TouristVoucher Voucher { get; set; }

        public Tourist()
        {

        }

        public Tourist(Tour tour, TouristAppearance touristAppearance , int locationId, int age, TouristVoucher voucher)
        {
            Tour = tour;
            TouristAppearance = touristAppearance;
            LocationId = locationId;
            Age = age;
            Voucher = voucher;
        }

        public Tourist(int id, Tour tour, TouristAppearance touristAppearance, int locationId, int age, TouristVoucher voucher)
        {
            Id = id;
            Tour = tour;
            TouristAppearance = touristAppearance;
            LocationId = locationId;
            Age = age;
            Voucher = voucher;
        }

        public Tourist(string username, string password, string name, string surname, string email, Role role, Tour tour, TouristAppearance touristAppearance, int locationId, int age, TouristVoucher voucher) : base(username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristAppearance = touristAppearance;
            LocationId = locationId;
            Age = age;
            Voucher = voucher;
        }


        public Tourist(int id, string username, string password, string name, string surname, string email, Role role, Tour tour, TouristAppearance touristAppearance, int locationId, int age, TouristVoucher voucher) : base(id, username, password, name, surname, email, role)
        {
            Tour = tour;
            TouristAppearance = touristAppearance;
            LocationId = locationId;
            Age = age;
            Voucher = voucher;
        }

    }
}
