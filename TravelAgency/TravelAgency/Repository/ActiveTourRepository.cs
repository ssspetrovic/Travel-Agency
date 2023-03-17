using Microsoft.Data.Sqlite;
using System;
using System.Data;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class ActiveTourRepository : RepositoryBase, IActiveTourRepository
    {
        public void Add(ActiveTourModel activeTourModel)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var keyPointsList = "";
            var touristsList = "";

            foreach (var tour in activeTourModel.KeyPoints)
            {
                keyPointsList += tour.Key.ToString() + ":" + tour.Value.ToString() + ", ";
            }

            keyPointsList = keyPointsList.Remove(keyPointsList.Length - 2, 2);

            foreach (var tourist in activeTourModel.Tourists)
            {
                touristsList += tourist.Name + ", ";
            }

            touristsList = touristsList.Remove(touristsList.Length - 2, 2);

            const string insertStatement =
                @"insert into ActiveTour(Name, KeyPointsList, Tourists) values ($Name, $KeyPointsList, $Tourists)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);

            insertCommand.Parameters.AddWithValue("$Name", activeTourModel.Name);
            insertCommand.Parameters.AddWithValue("$KeyPointsList", keyPointsList);
            insertCommand.Parameters.AddWithValue("$Tourists", touristsList);
            insertCommand.ExecuteNonQuery();
        }

        public void Edit(ActiveTourModel activeTourModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public ActiveTourModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DataTable GetActiveTour(DataTable dt)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            dt.Load(selectCommand.ExecuteReader());
            return dt;
        }

        public bool IsActive()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            return selectReader.Read();
        }

        public string GetActiveTourData(string column)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement = "select " + column + " from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            return selectReader.Read() ? selectReader.GetString(0) : "Error";
        }
    }
}
