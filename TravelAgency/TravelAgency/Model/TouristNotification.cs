namespace TravelAgency.Model
{
    public enum NotificationStatus
    {
        Read,
        Unread
    }

    class TouristNotification
    {
        public string? TouristUsername { get; set; }
        public string? Notification { get; set; }
        public NotificationStatus? Status { get; set; }
    }
}
