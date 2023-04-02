using Microsoft.Data.Sqlite;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class FinishedTourRepository : RepositoryBase, IFinishedTourRepository
    {
        public string GetAllKeyPoints(FinishedTour finishedTour)
        {
            var allKeyPoints = "";

            foreach (var keyPoint in finishedTour.KeyPoints)
            {
                allKeyPoints += keyPoint!.City + ", ";
            }

            allKeyPoints = allKeyPoints.Remove(allKeyPoints.Length - 2, 2);

            return allKeyPoints;
        }

        public string GetAllTourists(FinishedTour finishedTour)
        {
            var allTourists = "";

            foreach (var tourist in finishedTour.Tourists)
            {
                allTourists += tourist.UserName + ", ";
            }

            allTourists = allTourists.Remove(allTourists.Length - 2, 2);

            return allTourists;
        }


        public void Add(FinishedTour finishedTour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into FinishedTour(Name, KeyPointsList, Tourists) values ($Name, $KeyPointsList, $Tourists)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);

            insertCommand.Parameters.AddWithValue("$Name", finishedTour.Name);
            insertCommand.Parameters.AddWithValue("$KeyPointsList", GetAllKeyPoints(finishedTour));
            insertCommand.Parameters.AddWithValue("$Tourists", GetAllTourists(finishedTour));
            insertCommand.ExecuteNonQuery();
        }

        public void Edit(FinishedTour finishedTour)
        {
            throw new System.NotImplementedException();
        }

        public void Remove()
        {
            throw new System.NotImplementedException();
        }

        public FinishedTour GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckExistingTours(Tour tour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from FinishedTour where Id = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            selectCommand.Parameters.AddWithValue("$Id", tour.Id);
            var selectReader = selectCommand.ExecuteReader();

            return selectReader.Read();
        }
    }
}
