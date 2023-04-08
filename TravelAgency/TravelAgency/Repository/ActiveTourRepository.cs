using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Windows;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class ActiveTourRepository : RepositoryBase
    {

        public string GetAllKeyPoints(ActiveTour activeTour)
        {
            var allKeyPoints = "";
            activeTour.KeyPoints[activeTour.KeyPoints.First().Key] = true;

            foreach (var tour in activeTour.KeyPoints)
            {
                allKeyPoints += tour.Key.ToString() + ":" + tour.Value + ", ";
            }

            allKeyPoints = allKeyPoints.Remove(allKeyPoints.Length - 2, 2);

            return allKeyPoints;
        }

        public string GetAllTourists(ActiveTour activeTour)
        {
            var allTourists = "";

            foreach (var tourist in activeTour.Tourists)
            {
                allTourists += tourist.UserName + ", ";
            }

            allTourists = allTourists.Remove(allTourists.Length - 2, 2);

            return allTourists;
        }

        public void Add(ActiveTour activeTour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into ActiveTour(Name, KeyPointsList, Tourists) values ($Name, $KeyPointsList, $Tourists)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);

            insertCommand.Parameters.AddWithValue("$Name", activeTour.Name);
            insertCommand.Parameters.AddWithValue("$KeyPointsList", GetAllKeyPoints(activeTour));
            insertCommand.Parameters.AddWithValue("$Tourists", GetAllTourists(activeTour));
            insertCommand.ExecuteNonQuery();
        }

        public void Edit(ActiveTour activeTour)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            //Posto moze samo jedna tura da bude aktivna, brisemo sve iz tabele
            const string deleteStatement = "delete from ActiveTour";
            using var deleteCommand = new SqliteCommand(deleteStatement, databaseConnection);
            deleteCommand.ExecuteNonQuery();
        }

        public ActiveTour GetById(int id)
        {
            throw new NotImplementedException();
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

        public string FindCurrentKeyPoint(string currentKeyPoint, string keyPoints)
        {
            var allKeyPoints = keyPoints.Split(", ").ToList();
            var lastKeyPoint = "";
            var locationRepository = new LocationRepository();

            for (var i = 0; i < allKeyPoints.Capacity; i++)
            {
                var location = allKeyPoints[i];

                if (location.Contains(currentKeyPoint + ":True"))
                {
                    MessageBox.Show("We already passed " + locationRepository.GetById(int.Parse(currentKeyPoint))?.City + "!");
                    break;
                }

                if (location.Contains(currentKeyPoint + ":False"))
                    if (lastKeyPoint.Contains(":True"))
                    {
                        allKeyPoints[i] = currentKeyPoint + ":True";
                        break;
                    }
                    else
                    {
                        MessageBox.Show("You can't skip key points!");
                        break;
                    }

                lastKeyPoint = allKeyPoints[i];
            }

            return string.Join(", ", allKeyPoints);
        }

        public void UpdateKeyPoint(string currentKeyPoint)
        {
            var locationRepository = new LocationRepository();
            currentKeyPoint = locationRepository.GetByCity(currentKeyPoint)!.Id.ToString();

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select KeyPointsList from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var keyPoints = "";

            if (selectReader.Read())
            {
                keyPoints = FindCurrentKeyPoint(currentKeyPoint, selectReader.GetString(0));
            }

            databaseConnection.Close();
            databaseConnection.Open();


            const string updateStatement = "update ActiveTour set KeyPointsList = $KeyPointsList";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("KeyPointsList", keyPoints);
            updateCommand.ExecuteNonQuery();

        }
    }
}
