using System.Collections.ObjectModel;
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
                    INSERT INTO TouristNotification (TouristUsername, NotificationText, Status, Type)
                    VALUES ($TouristUsername, $NotificationText, $Status, $Type)
                ";
            insertCommand.Parameters.AddWithValue("$TouristUsername", notification.TouristUsername);
            insertCommand.Parameters.AddWithValue("$NotificationText", notification.NotificationText);
            insertCommand.Parameters.AddWithValue("$TouristUsername", notification.Status);
            insertCommand.Parameters.AddWithValue("$Type", notification.Type);
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var deleteCommand = databaseConnection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM TouristNotification WHERE Id = $Id";
            deleteCommand.Parameters.AddWithValue("$Id", id);
            deleteCommand.ExecuteNonQuery();
        }

        public ObservableCollection<TouristNotification> GetAllAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM TouristNotification WHERE TouristUsername = $CurrentUserUsername";
            selectCommand.Parameters.AddWithValue("$CurrentUserUsername", CurrentUser.Username);
            using var selectReader = selectCommand.ExecuteReader();

            var vouchers = new ObservableCollection<TouristNotification>();
            while (selectReader.Read())
            {
                vouchers.Add(new TouristNotification(
                    selectReader.GetInt32(0),
                    selectReader.GetString(1),
                    selectReader.GetString(2),
                    (NotificationStatus)selectReader.GetInt32(3),
                    (NotificationType)selectReader.GetInt32(4)
                ));
            }

            return vouchers;
        }

        public void UpdateStatus(int id, NotificationStatus newStatus)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE MyTourDto
                    SET Status = $newStatus
                    WHERE Id = $id
                ";

            updateCommand.Parameters.AddWithValue("$id", id);
            updateCommand.Parameters.AddWithValue("$newStatus", newStatus);
            updateCommand.ExecuteNonQuery();
        }
    }
}
