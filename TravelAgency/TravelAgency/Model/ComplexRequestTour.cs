namespace TravelAgency.Model
{
    public class ComplexRequestTour : RequestTour
    {
        public string RequestTourIds { get; set; }

        public ComplexRequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status, string requestTourIds) : base(id, location, description, language, maxGuests, dateRange, status)
        {
            RequestTourIds = requestTourIds;
        }

        public ComplexRequestTour(int id, Location location, string description, Language language, int maxGuests, string dateRange, Status status, string acceptedDate, string requestTourIds) : base(id, location, description, language, maxGuests, dateRange, status, acceptedDate)
        {
            RequestTourIds = requestTourIds;
        }

        public ComplexRequestTour(int id, string requestTourIds, string dateRange, Location location, string description, Language language, int maxGuests, Status status, string acceptedDate, string touristUsername) : base(id, location, description, language, maxGuests, dateRange, status, acceptedDate, touristUsername)
        {
            RequestTourIds = requestTourIds;
        }
        public ComplexRequestTour(string requestTourIds, string dateRange, Location location, string description, Language language, int maxGuests, Status status, string acceptedDate, string touristUsername) : base(location, description, language, maxGuests, dateRange, status, acceptedDate, touristUsername)
        {
            RequestTourIds = requestTourIds;
        }
    }
}
