using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Collections.ObjectModel;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Service;

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
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

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
                    INSERT INTO RequestedTour (Location_Id, Language, DateRange, NumberOfGuests, Description, Status, TouristUsername)
                    VALUES  ($Location_Id, $Language, $DateRange, $GuestNumber, $Description, $Status, $TouristUsername)
                ";
            insertCommand.Parameters.AddWithValue("$Location_Id", requestTour.Location.Id);
            insertCommand.Parameters.AddWithValue("$Language", (int)requestTour.Language);
            insertCommand.Parameters.AddWithValue("$DateRange", requestTour.DateRange);
            insertCommand.Parameters.AddWithValue("$GuestNumber", requestTour.MaxGuests);
            insertCommand.Parameters.AddWithValue("$Description", requestTour.Description);
            insertCommand.Parameters.AddWithValue("$Status", (int)requestTour.Status);
            insertCommand.Parameters.AddWithValue("$TouristUsername", requestTour.TouristUsername);
            insertCommand.ExecuteNonQuery();
        }

        public ObservableCollection<RequestTour> GetAllForSelectedYearAsCollection(string? year = null, string? touristUsername = null)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();

            if (year == null && touristUsername == null)
            {
                selectCommand.CommandText = "SELECT * FROM RequestedTour";
            }

            if (year is "All years" or null)
            {
                selectCommand.CommandText = "SELECT * FROM RequestedTour WHERE TouristUsername = $CurrentUserUsername";
                selectCommand.Parameters.AddWithValue("$CurrentUserUsername", CurrentUser.Username);

            }
            else
            {
                selectCommand.CommandText =
                    "SELECT * FROM RequestedTour WHERE TouristUsername = $CurrentUserUsername AND SUBSTR(DateRange, 7, 4) = $year OR SUBSTR(DateRange, -4, 4) = $year";
                selectCommand.Parameters.AddWithValue("$year", year);
                selectCommand.Parameters.AddWithValue("$CurrentUserUsername", CurrentUser.Username);

            }

            using var selectReader = selectCommand.ExecuteReader();
            var locationService = new LocationService();

            var requests = new ObservableCollection<RequestTour>();
            while (selectReader.Read())
            {
                string? selectedDate = null;
                if (!selectReader.IsDBNull(7))
                    selectedDate = selectReader.GetString(7);

                requests.Add(new RequestTour(
                    selectReader.GetInt32(0),
                    locationService.GetById(selectReader.GetInt32(1))!,
                    selectReader.GetString(2),
                    (Language)selectReader.GetInt32(3),
                    selectReader.GetInt32(4),
                    selectReader.GetString(5),
                    (Status)selectReader.GetInt32(6),
                    selectedDate,
                    selectReader.GetString(8)
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
                    SUBSTR(DateRange, -4, 4) AS EndYear FROM RequestedTour
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

        public void UpdateStatusById(int id, Status newStatus)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE RequestedTour
                    SET Status = $newStatus
                    WHERE Id = $id
                ";
            updateCommand.Parameters.AddWithValue("$newStatus", newStatus);
            updateCommand.Parameters.AddWithValue("$id", id);

            updateCommand.ExecuteNonQuery();
        }

        public RequestTour GetById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM RequestedTour WHERE Id = $id";
            selectCommand.Parameters.AddWithValue("$id", id);

            using var selectReader = selectCommand.ExecuteReader();
            if (!selectReader.Read())
            {
                throw new Exception("Failed to select data from table.");
            }

            var locationService = new LocationService();

            string? selectedDate = null;
            if (!selectReader.IsDBNull(7))
                selectedDate = selectReader.GetString(7);

            return new RequestTour(
                selectReader.GetInt32(0),
                locationService.GetById(selectReader.GetInt32(1))!,
                selectReader.GetString(2),
                (Language)selectReader.GetInt32(3),
                selectReader.GetInt32(4),
                selectReader.GetString(5),
                (Status)selectReader.GetInt32(6),
                selectedDate,
                selectReader.GetString(8)
            );
        }

        public void UpdateAcceptedDateById(int id, string acceptedDate)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE RequestedTour
                    SET AcceptedDate = $acceptedDate
                    WHERE Id = $id
                ";

            updateCommand.Parameters.AddWithValue("$acceptedDate", acceptedDate);
            updateCommand.Parameters.AddWithValue("$id", id);
            updateCommand.ExecuteNonQuery();
        }
    }
}
