using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;
using TravelAgency.DTO;
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
                    INSERT INTO TouristNotification (TouristUsername, NotificationText, tourName, Status, Type)
                    VALUES ($TouristUsername, $NotificationText, $TourName, $Status, $Type)
                ";
            insertCommand.Parameters.AddWithValue("$TouristUsername", notification.TouristUsername);
            insertCommand.Parameters.AddWithValue("$NotificationText", notification.NotificationText);
            insertCommand.Parameters.AddWithValue("$TourName", notification.TourName);
            insertCommand.Parameters.AddWithValue("$Status", notification.Status);
            insertCommand.Parameters.AddWithValue("$Type", notification.Type);

            try
            {
                insertCommand.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                if (ex.SqliteErrorCode == 19) { /* nothing */ }
            }
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

        public ObservableCollection<TouristNotificationDto> GetAllDtoAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM TouristNotification WHERE TouristUsername = $CurrentUserUsername";
            selectCommand.Parameters.AddWithValue("$CurrentUserUsername", CurrentUser.Username);
            using var selectReader = selectCommand.ExecuteReader();

            var vouchers = new ObservableCollection<TouristNotificationDto>();
            while (selectReader.Read())
            {
                vouchers.Add(new TouristNotificationDto(
                    selectReader.GetInt32(0),
                    selectReader.GetString(1),
                    selectReader.GetString(2),
                    selectReader.GetString(3),
                    (NotificationStatus)selectReader.GetInt32(4),
                    (NotificationType)selectReader.GetInt32(5)
                ));
            }

            return vouchers;
        }

        public bool IsDuplicate(string? username, string? text)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM TouristNotification WHERE TouristUsername = $username AND NotificationText = $text";
            selectCommand.Parameters.AddWithValue("$username", username);
            selectCommand.Parameters.AddWithValue("$text", text);
            using var selectReader = selectCommand.ExecuteReader();

            return selectReader.Read();
        }

        public void UpdateStatus(int id, NotificationStatus newStatus)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE TouristNotification
                    SET Status = $newStatus
                    WHERE Id = $id
                ";

            updateCommand.Parameters.AddWithValue("$id", id);
            updateCommand.Parameters.AddWithValue("$newStatus", newStatus);
            updateCommand.ExecuteNonQuery();
        }

        public void UpdateType(int id, NotificationType newType)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE TouristNotification
                    SET Type = $newType
                    WHERE Id = $id
                ";

            updateCommand.Parameters.AddWithValue("$id", id);
            updateCommand.Parameters.AddWithValue("$newType", newType);
            updateCommand.ExecuteNonQuery();
        }
    }
}
