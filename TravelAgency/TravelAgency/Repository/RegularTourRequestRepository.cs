using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelAgency.Model;
using static TravelAgency.Model.RegularTourRequest;

namespace TravelAgency.Repository
{
    internal class RegularTourRequestRepository : RepositoryBase
    {
        public void Add(RegularTourRequest tourRequest)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO RegularTourRequest (TouristUsername, City, Country, Language, DateRange, GuestNumber, Description, Status)
                    VALUES ($TouristUsername, $City, $Country, $Language, $DateRange, $GuestNumber, $Description, $Status)
                ";
            insertCommand.Parameters.AddWithValue("$TouristUsername", tourRequest.TouristUsername);
            insertCommand.Parameters.AddWithValue("$City", tourRequest.Location.City);
            insertCommand.Parameters.AddWithValue("$Country", tourRequest.Location.Country);
            insertCommand.Parameters.AddWithValue("$Language", (int)tourRequest.Language!);
            insertCommand.Parameters.AddWithValue("$DateRange", tourRequest.DateRange);
            insertCommand.Parameters.AddWithValue("$GuestNumber", tourRequest.GuestNumber);
            insertCommand.Parameters.AddWithValue("$Description", tourRequest.Description);
            insertCommand.Parameters.AddWithValue("$Status", tourRequest.Status);
            insertCommand.ExecuteNonQuery();
        }

        public void UpdateStatus(int id, RegularTourRequest.TourRequestStatus newStatus)
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

        public ObservableCollection<RegularTourRequest> GetAllForSelectedYearAsCollection(string? year = null)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();

            if (year is "All years" or null)
            {
                selectCommand.CommandText = "SELECT * FROM RegularTourRequest WHERE TouristUsername = $CurrentUserUsername";
            }
            else
            {
                selectCommand.CommandText =
                    "SELECT * FROM RegularTourRequest WHERE TouristUsername = $CurrentUserUsername AND SUBSTR(DateRange, 7, 4) = $year OR SUBSTR(DateRange, -4, 4) = $year";
                selectCommand.Parameters.AddWithValue("$year", year);
            }

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

        public ObservableCollection<string> GetAllYears()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText =
                @"
                    SELECT DISTINCT
                    SUBSTR(DateRange, 7, 4) AS StartYear,
                    SUBSTR(DateRange, -4, 4) AS EndYear FROM RegularTourRequest
                ";
            var selectReader = selectCommand.ExecuteReader();

            var years = new List<string>();
            while (selectReader.Read())
            {
                years.Add(selectReader.GetString(0));
                years.Add(selectReader.GetString(1));
            }

            var distinctYears = years.Distinct().ToList();
            return new ObservableCollection<string>(distinctYears);
        }
    }
}
