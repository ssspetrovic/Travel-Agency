﻿using System.Collections.ObjectModel;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class RegularTourRequestRepository : RepositoryBase
    {
        public void AddRegular(RegularTourRequest tourRequest)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO RegularTourRequest (TouristUsername, City, Country, Language, DateRange, Description, Status)
                    VALUES ($TouristUsername, $City, $Country, $Language, $DateRange, $Description, $Status)
                ";
            insertCommand.Parameters.AddWithValue("$TouristUsername", tourRequest.TouristUsername);
            insertCommand.Parameters.AddWithValue("$City", tourRequest.Location.City);
            insertCommand.Parameters.AddWithValue("$Country", tourRequest.Location.Country);
            insertCommand.Parameters.AddWithValue("$Language", (int)tourRequest.Language!);
            insertCommand.Parameters.AddWithValue("$DateRange", tourRequest.DateRange);
            insertCommand.Parameters.AddWithValue("$Description", tourRequest.Description);
            insertCommand.Parameters.AddWithValue("$Status", tourRequest.Status);
            insertCommand.ExecuteNonQuery();
        }

        public void UpdateStatusRegular(int id, RegularTourRequest.TourRequestStatus newStatus)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE RegularTourRequest
                    SET Status = $newStatus
                    WHERE Id = $id
                ";

            updateCommand.Parameters.AddWithValue("$newStatus", (int)newStatus);
            updateCommand.Parameters.AddWithValue("$id", id);
            updateCommand.ExecuteNonQuery();
        }

        public ObservableCollection<RegularTourRequest> GetAllRegularAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM RegularTourRequest WHERE TouristUsername = $CurrentUserUsername";
            selectCommand.Parameters.AddWithValue("$CurrentUserUsername", CurrentUser.Username);
            using var selectReader = selectCommand.ExecuteReader();

            var requests = new ObservableCollection<RegularTourRequest>();
            while (selectReader.Read())
            {
                requests.Add(new RegularTourRequest(
                    selectReader.GetInt32(0),
                    selectReader.GetString(1),
                    new Location(selectReader.GetString(2), selectReader.GetString(3)),
                    (Language)selectReader.GetInt32(4),
                    selectReader.GetInt32(5),
                    selectReader.GetString(6),
                    selectReader.GetString(7),
                    (RegularTourRequest.TourRequestStatus)selectReader.GetInt32(8)
                ));
            }

            return requests;
        }
    }
}
