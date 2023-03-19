using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Linq;
using System.Windows;
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

            activeTourModel.KeyPoints[activeTourModel.KeyPoints.First().Key] = true;

            foreach (var tour in activeTourModel.KeyPoints)
            {
                keyPointsList += tour.Key.ToString() + ":" + tour.Value.ToString() + ", ";
            }

            keyPointsList = keyPointsList.Remove(keyPointsList.Length - 2, 2);

            foreach (var tourist in activeTourModel.Tourists)
            {
                touristsList += tourist.UserName + ", ";
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

        public void Remove()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string deleteStatement = "delete from ActiveTour";
            using var deleteCommand = new SqliteCommand(deleteStatement, databaseConnection);
            deleteCommand.ExecuteNonQuery();
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

        public void UpdateKeyPoint(string keyPoint)
        {
            var locationRepository = new LocationRepository();
            keyPoint = locationRepository.GetByCity(keyPoint)!.Id.ToString();

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select KeyPointsList from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var keyPoints = "";

            if (selectReader.Read())
            {
                keyPoints = selectReader.GetString(0);
            }

            databaseConnection.Close();
            databaseConnection.Open();

            var keyPointsList = keyPoints.Split(", ").ToList();
            var lastKeyPoint = "";

            for (var i = 0; i < keyPointsList.Capacity; i++)
            {
                var location = keyPointsList[i];

                if (location.Contains(keyPoint + ":False"))
                    if (lastKeyPoint.Contains(":True"))
                    {
                        keyPointsList[i] = keyPoint + ":True";
                        break;
                    }
                    else
                    {
                        MessageBox.Show("You can't skip key points!");
                        break;
                    }
                else if(location.Contains(keyPoint + ":True"))
                {
                    MessageBox.Show("We already passed this key point!");
                    break;
                }

                lastKeyPoint = keyPointsList[i];
            }

            keyPoints = string.Join(", ", keyPointsList);

            const string updateStatement = "update ActiveTour set KeyPointsList = $KeyPointsList";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("KeyPointsList", keyPoints);
            updateCommand.ExecuteNonQuery();

        }
    }
}
