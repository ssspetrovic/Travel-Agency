namespace TravelAgency.Model
{
    public enum NotificationStatus
    {
        Read,
        Unread
    }

    public enum NotificationType
    {
        NewOffer,
        RequestAccepted,
        NewVoucher
    }

    class TouristNotification
    {
        public int Id { get; set; }
        public string TouristUsername { get; set; }
        public string NotificationText { get; set; }
        public NotificationStatus Status { get; set; }
        public NotificationType Type { get; set; }

        public TouristNotification(int id, string touristUsername, string notificationText, NotificationStatus status, NotificationType type)
        {
            Id = id;
            TouristUsername = touristUsername;
            NotificationText = notificationText;
            Status = status;
            Type = type;
        }
    }
}
