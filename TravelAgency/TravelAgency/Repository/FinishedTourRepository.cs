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
                @"insert into FinishedTour(Name, KeyPointsList, Tourists, Date) values ($Name, $KeyPointsList, $Tourists, $Date)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);

            insertCommand.Parameters.AddWithValue("$Name", finishedTour.Name);
            insertCommand.Parameters.AddWithValue("$KeyPointsList", GetAllKeyPoints(finishedTour));
            insertCommand.Parameters.AddWithValue("$Tourists", GetAllTourists(finishedTour));
            insertCommand.Parameters.AddWithValue("$Date", finishedTour.Date);
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

        public ObservableCollection<Tour> GetBestTour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Tour where MaxGuests = (select max(MaxGuests) from Tour)";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var tourList = new ObservableCollection<Tour>();
            var locationRepository = new LocationRepository();


            while (selectReader.Read())
            {
                var location = locationRepository.GetById(selectReader.GetInt32(2));

                tourList.Add(new Tour(selectReader.GetInt32(0), selectReader.GetString(1),
                    location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                    GetKeyPoints(selectReader.GetString(6)), selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9)));
            }

            return tourList;
        }

        public ChartValues<int> GetAgeGroup (List<Tourist> tourist)
        { 
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var ageGroup = new ChartValues<int> {0, 0, 0};
            var ids = "";

            foreach (var t in tourist)
                ids += t.Id + ", ";
            ids = ids.Remove(ids.Length - 2, 2);

            var selectStatement = "select * from Tourist where Id in (" + ids + ")";
            using var selectCommand = new SqliteCommand( selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
            {
                if(selectReader.GetInt32(10) < 19)
                    ageGroup[0]++;
                else if (selectReader.GetInt32(10) < 50)
                    ageGroup[1]++;
                else
                    ageGroup[2]++;
            }


            return ageGroup;
        }
    }
}
