using LiveCharts;
using LiveCharts.Defaults;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
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
                allKeyPoints += keyPoint!.Id + ", ";
            }

            allKeyPoints = allKeyPoints.Remove(allKeyPoints.Length - 2, 2);

            return allKeyPoints;
        }

        public List<Location?> GetAllKeyPointsByIds(string keyPoints)
        {
            var locationRepository = new LocationRepository();
            var locations = keyPoints.Split(", ").ToList();
            var retVal = new List<Location?>();

            foreach (var location in locations)
                retVal.Add(locationRepository.GetById(int.Parse(location)));

            return retVal;
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

        public List<Tourist> GetAllTouristsByNames(string names)
        {
            var touristRepository = new TouristRepository();
            var tourists = names.Split(", ").ToList();
            var retVal = new List<Tourist>();

            foreach(var tourist in tourists)
                retVal.Add(touristRepository.GetByUsername(tourist));

            return retVal;
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

        public ObservableCollection<FinishedTour> GetBestTour(SqliteCommand selectCommand)
        {
            using var selectReader = selectCommand.ExecuteReader();
            var finishedTours = new ObservableCollection<FinishedTour>();
            while (selectReader.Read())
                finishedTours.Add(new FinishedTour(selectReader.GetInt32(0), selectReader.GetString(1), GetKeyPoints(selectReader.GetString(2)), FinishedTourTourists(selectReader.GetString(3)), selectReader.GetString(4)));

            return finishedTours;
        }

        public ObservableCollection<FinishedTour> GetAllTimeBestTour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from FinishedTour where TouristNumber = (select max(TouristNumber) from FinishedTour)";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            return GetBestTour(selectCommand);
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

        public ObservableCollection<FinishedTour> GetBestOf2022Tour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from FinishedTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            return FindBestTourByYear(GetBestTour(selectCommand), "2022");
        }

        public ObservableCollection<FinishedTour> GetBestOf2023Tour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from FinishedTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            return FindBestTourByYear(GetBestTour(selectCommand), "2023");
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

        internal ChartValues<ObservableValue> GetVoucherOdds(FinishedTour finishedTour)
        {
            var withVoucher = 0;
            var withoutVoucher = 0;

            foreach (var tourist in finishedTour.Tourists)
                if (tourist.Voucher != TouristVoucher.None)
                    withVoucher++;
                else
                    withoutVoucher++;

            var voucherOdds = new ChartValues<ObservableValue> { new(withVoucher), new(withoutVoucher) };
            return voucherOdds;
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

        public FinishedTour FindFinishedTour(string name)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from FinishedTour where Name = $Name";
            using var selectCommand = new SqliteCommand( selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Name", name);

            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read())
                return new FinishedTour();

            return new FinishedTour(selectReader.GetInt32(0), selectReader.GetString(1),
                GetAllKeyPointsByIds(selectReader.GetString(2)), GetAllTouristsByNames(selectReader.GetString(3)),
                selectReader.GetString(4));
        }
    }
}
