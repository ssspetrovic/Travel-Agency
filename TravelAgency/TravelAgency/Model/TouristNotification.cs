namespace TravelAgency.Model
{
    public enum NotificationStatus
    {
        Read,
        Unread
    }

    class TouristNotification
    {
        public int Id { get; set; }
        public string? TouristUsername { get; set; }
        public string? NotificationText { get; set; }
        public NotificationStatus? Status { get; set; }
    }
}
