using LiveCharts;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public List<Location?> GetKeyPoints(string keyPoints)
        {
            var locationRepository = new LocationRepository();
            var locations = keyPoints.Split(", ").ToList();
            var retVal = new List<Location?>();

            foreach (var location in locations)
                retVal.Add(locationRepository.GetById(int.Parse(location)));

            return retVal;
        }

        public List<Tourist> FinishedTourTourists(string tourists)
        {
            var finishedTourTourists = new List<Tourist>();
            var touristRepository = new TouristRepository();

            foreach (var tourist in tourists.Split(", "))
                finishedTourTourists.Add(touristRepository.GetByUsername(tourist));

            return finishedTourTourists;
        }

        public void GetBestTour(ObservableCollection<FinishedTour> finishedTour, SqliteCommand selectCommand)
        {
            using var selectReader = selectCommand.ExecuteReader();
            while (selectReader.Read())
                finishedTour.Add(new FinishedTour(selectReader.GetInt32(0), selectReader.GetString(1), GetKeyPoints(selectReader.GetString(2)), FinishedTourTourists(selectReader.GetString(3)), selectReader.GetString(4)));
            
        }

        public ObservableCollection<FinishedTour> GetAllTimeBestTour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from FinishedTour where TouristNumber = (select max(TouristNumber) from FinishedTour)";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            var finishedTour = new ObservableCollection<FinishedTour>();

            GetBestTour(finishedTour, selectCommand);

            return finishedTour;
        }

        public ObservableCollection<FinishedTour> GetBestOf2022Tour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from FinishedTour where TouristNumber = (select max(TouristNumber) from FinishedTour)";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            var finishedTour = new ObservableCollection<FinishedTour>();

            GetBestTour(finishedTour, selectCommand);

            return finishedTour;
        }

        public ObservableCollection<FinishedTour> GetBestOf2023Tour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from FinishedTour where TouristNumber = (select max(TouristNumber) from FinishedTour)";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            var finishedTour = new ObservableCollection<FinishedTour>();

            GetBestTour(finishedTour, selectCommand);

            return finishedTour;
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
    }
}
