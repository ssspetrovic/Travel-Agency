namespace TravelAgency.Model
{
    public class TourReservation
    {
        public int Id { get; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public int GuestNumber { get; set; }
        public string TouristUsername { get; set; }
        public string TouristName{ get; set; }

        public TourReservation(int tourId, string tourName, int guestNumber, string touristUsername, string touristName)
        {
            TourId = tourId;
            TourName = tourName;
            GuestNumber = guestNumber;
            TouristUsername = touristUsername;
            TouristName = touristName;
        }
    }
}
