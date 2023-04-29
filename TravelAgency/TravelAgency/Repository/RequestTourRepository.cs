using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;

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
    }
}
