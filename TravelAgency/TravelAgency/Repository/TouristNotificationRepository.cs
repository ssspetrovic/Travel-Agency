using TravelAgency.Model;

namespace TravelAgency.Repository
{
    class TouristNotificationRepository : RepositoryBase
    {
        public void Add(TouristNotification notification)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO TouristNotification TouristUsername, NotificationText, Status)
                    VALUES ($TouristUsername, $NotificationText, $Status)
                ";
            insertCommand.Parameters.AddWithValue("$TouristUsername", notification.TouristUsername);
            insertCommand.Parameters.AddWithValue("$NotificationText", notification.NotificationText);
            insertCommand.Parameters.AddWithValue("$TouristUsername", notification.Status);
            insertCommand.ExecuteNonQuery();
        }
    }
}
