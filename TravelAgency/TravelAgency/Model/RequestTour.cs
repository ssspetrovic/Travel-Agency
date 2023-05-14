namespace TravelAgency.Model
{
    public enum Status
    {
        OnHold,
        Accepted,
        Invalid
    }

    public class RequestTour : Tour
    {
        public string DateRange { get; set; }
        public Status Status { get; set; }
        public string AcceptedDate { get; set; } = null!;

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
    }
}
