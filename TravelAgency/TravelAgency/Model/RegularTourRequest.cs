namespace TravelAgency.Model
{
    internal class RegularTourRequest
    {
        public enum TourRequestStatus
        {
            Invalid,
            OnHold,
            Accepted
        }

        public int Id { get; set; }
        public string? TouristUsername { get; set; }
        public Location Location { get; set; }
        public Language? Language { get; set; }
        public int GuestNumber { get; set; }
        public string DateRange { get; set; }
        public string Description { get; set; }
        public TourRequestStatus Status { get; set; }

        public RegularTourRequest(string? touristUsername, Location location, Language? language, int guestNumber, string dateRange, string description, TourRequestStatus status)
        {
            TouristUsername = touristUsername;
            Location = location;
            Language = language;
            GuestNumber = guestNumber;
            DateRange = dateRange;
            Description = description;
            Status = status;
        }

        public RegularTourRequest(int id, string? touristUsername, Location location, Language? language, int guestNumber, string dateRange, string description, TourRequestStatus status)
        {
            Id = id;
            TouristUsername = touristUsername;
            Location = location;
            Language = language;
            GuestNumber = guestNumber;
            DateRange = dateRange;
            Description = description;
            Status = status;
        }
    }
}
