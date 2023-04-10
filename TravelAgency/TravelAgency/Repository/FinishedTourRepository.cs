using LiveCharts;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.Data;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class FinishedTourRepository : RepositoryBase
    {
        public string GetAllKeyPoints(FinishedTour finishedTour)
        {
            var allKeyPoints = "";

            foreach (var keyPoint in finishedTour.KeyPoints)
            {
                allKeyPoints += keyPoint!.Id + ", ";
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
                @"insert into FinishedTour(Name, KeyPointsList, Tourists, Date, TouristNumber) values ($Name, $KeyPointsList, $Tourists, $Date, $TouristNumber)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);

            insertCommand.Parameters.AddWithValue("$Name", finishedTour.Name);
            insertCommand.Parameters.AddWithValue("$KeyPointsList", GetAllKeyPoints(finishedTour));
            insertCommand.Parameters.AddWithValue("$Tourists", GetAllTourists(finishedTour));
            insertCommand.Parameters.AddWithValue("$Date", finishedTour.Date);
            insertCommand.Parameters.AddWithValue("$TouristNumber", finishedTour.Tourists.Count);
            insertCommand.ExecuteNonQuery();
        }

        public void Edit(FinishedTour finishedTour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            const string updateStatement = "update FinishedTour set Date = Date || ', ' || $NewDate";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("$NewDate", finishedTour.Date);
            updateCommand.ExecuteNonQuery();

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

        public ObservableCollection<FinishedTour> FindBestTourByYear(ObservableCollection<FinishedTour> allFinishedTours, string year)
        {
            var finishedToursByYear = new ObservableCollection<FinishedTour>();
            foreach (var finishedTour in allFinishedTours)
                if (finishedTour.Date.Contains(year))
                    finishedToursByYear.Add(finishedTour);

            var bestTour = new ObservableCollection<FinishedTour> { finishedToursByYear[0] };

            foreach (var finishedTour in finishedToursByYear)
                if (bestTour[0].Tourists.Count < finishedTour.Tourists.Count)
                    bestTour[0] = finishedTour;

            return bestTour;
        }

        public ChartValues<int> GetAgeGroup (FinishedTour finishedTour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var ageGroup = new ChartValues<int> { 0, 0, 0 };

            foreach (var tourist in finishedTour.Tourists)
                if(tourist.Age < 19)
                    ageGroup[0]++;
                else if (tourist.Age < 50)
                    ageGroup[1]++;
                else
                    ageGroup[2]++;

            return ageGroup;
        }

        public DataTable GetAllAsDataTable(DataTable dt)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from FinishedTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            dt.Load(selectCommand.ExecuteReader());
            return dt;
        }

        public string GetNewTourName()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from FinishedTour order by Id desc limit 1";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read()) return "No Tour";
            return selectReader.GetString(1);
        }
    }
}
