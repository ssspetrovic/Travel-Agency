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
        public bool IsComplex { get; set; }
        public int ComplexId { get; set; } = -1;

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


        public RequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status, string acceptedDate, string touristUsername)
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

        public RequestTour(Location location, string description, Language language, int maxGuests, string dateRange, Status status, string acceptedDate, string touristUsername)
        {
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DateRange = dateRange;
            Status = status;
            AcceptedDate = acceptedDate;
            TouristUsername = touristUsername;
        }

        public RequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status, bool isComplex)
        {
            Id = id;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DateRange = dateRange;
            Status = status;
            IsComplex = isComplex;
        }

        public RequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status, bool isComplex, int complexId)
        {
            Id = id;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DateRange = dateRange;
            Status = status;
            IsComplex = isComplex;
            ComplexId = complexId;
        }

        public RequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status, string acceptedDate, bool isComplex)
        {
            Id = id;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DateRange = dateRange;
            Status = status;
            AcceptedDate = acceptedDate;
            IsComplex = isComplex;
        }

        public RequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status, string? acceptedDate, string? touristUsername, bool isComplex)
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
            IsComplex = isComplex;
        }

        public RequestTour(Location? location, string description, Language? language, int maxGuests, string dateRange, Status status,  string? touristUsername, bool isComplex)
        {
            Location = location!;
            Description = description;
            Language = (Language)language!;
            MaxGuests = maxGuests;
            DateRange = dateRange;
            Status = status;
            TouristUsername = touristUsername;
            IsComplex = isComplex;
        }
    }
}
