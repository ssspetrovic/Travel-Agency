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
        public string TourName { get; set; }
        public NotificationStatus Status { get; set; }
        public NotificationType Type { get; set; }
        public bool IsChecked { get; set; }

        public TouristNotification(int id, string touristUsername, string notificationText, string tourName, NotificationStatus status, NotificationType type)
        {
            Id = id;
            TouristUsername = touristUsername;
            TourName = tourName;
            NotificationText = notificationText;
            Status = status;
            Type = type;
        }

        public TouristNotification(string touristUsername, string notificationText, string tourName, NotificationStatus status, NotificationType type)
        {
            TouristUsername = touristUsername;
            TourName = tourName;
            NotificationText = notificationText;
            Status = status;
            Type = type;
        }
    }
}
