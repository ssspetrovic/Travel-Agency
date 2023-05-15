using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Collections.ObjectModel;
using System.Linq;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class RequestTourRepository : RepositoryBase
    {
        public DataTable GetAllAsDataTable(DataTable dt)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from RequestedTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            dt.Load(selectCommand.ExecuteReader());
            return dt;
        }

        public string GetMostRequestedStat(string column)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement =
                "select " + column + ", count(*) as RequestedLocation from RequestedTour group by " + column + " order by RequestedLocation desc limit 1";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read()) return "No Requests!";
            return selectReader.GetString(0);
        }

        public DataTable UpdateDataTable(DataTable dt, string ids)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement = "select * from RequestedTour where Id in (" + ids + ")";
            using var selectCommand = new SqliteCommand( selectStatement, databaseConnection);

            dt.Clear();
            dt.Load(selectCommand.ExecuteReader());
            return dt;
        }

        public List<string> GetAllRequestedLanguages()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var languages = new List<string>();
            const string selectStatement = "select distinct Language from RequestedTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            using var selectReader = selectCommand.ExecuteReader();
            while (selectReader.Read())
                languages.Add(selectReader.GetString(0));

            return languages;
        }

        public void Add(RequestTour requestTour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO Req (TouristUsername, City, Country, Language, DateRange, GuestNumber, Description, Status)
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

        public ObservableCollection<string> GetAllYearsAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText =
                @"
                    SELECT DISTINCT
                    SUBSTR(DateRange, 7, 4) AS StartYear,
                    SUBSTR(DateRange, -4, 4) AS EndYear FROM RegularTourRequest
                    ORDER BY StartYear ASC, EndYear ASC
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
