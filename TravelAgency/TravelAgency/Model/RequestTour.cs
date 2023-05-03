namespace TravelAgency.Model
{
    public enum Status
    {
        OnHold,
        Accepted,
        Invalid
    }

    public class RequestTour
    {
        public int Id { get; set; }
        public Location Location {get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
        public int NumberOfGuests { get; set; }
        public string DateRange { get; set; }
        public Status Status { get; set; }
        public string AcceptedDate { get; set; } = null!;

        public RequestTour(int id, Location location, string description, Language language, int numberOfGuests, string dateRange, Status status)
        {
            Id = id;
            Location = location;
            Description = description;
            Language = language;
            NumberOfGuests = numberOfGuests;
            DateRange = dateRange;
            Status = status;
        }

        public RequestTour(int id, Location location, string description, Language language, int numberOfGuests, string dateRange, Status status, string acceptedDate)
        {
            Id = id;
            Location = location;
            Description = description;
            Language = language;
            NumberOfGuests = numberOfGuests;
            DateRange = dateRange;
            Status = status;
            AcceptedDate = acceptedDate;
        }
    }
}
