using Microsoft.Data.Sqlite;
using System.Linq;
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
                allKeyPoints += tour.Key + ":" + tour.Value + ", ";
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

        public void Remove()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            
            const string deleteStatement = "delete from ActiveTour";
            using var deleteCommand = new SqliteCommand(deleteStatement, databaseConnection);
            deleteCommand.ExecuteNonQuery();
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
    }
}
