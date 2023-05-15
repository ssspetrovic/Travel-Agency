namespace TravelAgency.Model
{
    public enum Status
    {
        OnHold,
        Accepted,
        Invalid,
        Updating
    }

    public class RequestTour : Tour
    {
        public string DateRange { get; set; }
        public Status Status { get; set; }
        public string? AcceptedDate { get; set; }
        public string? TouristUsername { get; set; }

        public RequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status)
        {
            Id = id;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DateRange = dateRange;
            Status = status;
        }

        public RequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status, string acceptedDate)
        {
            Id = id;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DateRange = dateRange;
            Status = status;
            AcceptedDate = acceptedDate;
        }

        public RequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status, string? acceptedDate, string? touristUsername)
        {
            Id = id;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DateRange = dateRange;
            Status = status;
            AcceptedDate = acceptedDate;
            TouristUsername = touristUsername;
        }

        public RequestTour(Location? location, string description, Language? language, int maxGuests, string dateRange, Status status,  string? touristUsername)
        {
            Location = location!;
            Description = description;
            Language = (Language)language!;
            MaxGuests = maxGuests;
            DateRange = dateRange;
            Status = status;
            TouristUsername = touristUsername;
        }
    }
}
