using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;
using Microsoft.Data.Sqlite;
using TravelAgency.Interface;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class FinishedTourService : RepositoryBase
    {
        private readonly IFinishedTourRepository _finishedTourRepository;
        private readonly LocationService _locationService;
        private readonly TouristService _touristService;
        public FinishedTourService()
        {
            _finishedTourRepository = new FinishedTourRepository();
            _locationService = new LocationService();
            _touristService = new TouristService();
        }

        public DataTable GetAllAsDataTable(DataTable dataTable)
        {
            return _finishedTourRepository.GetAllAsDataTable(dataTable);
        }

        public bool CheckExistingTours(Tour tour)
        {
            return _finishedTourRepository.CheckExistingTours(tour);
        }

        public void Edit(Tour finishedTour)
        {
            _finishedTourRepository.Edit(finishedTour);
        }

        public void Add(Tour finishedTour)
        {
            _finishedTourRepository.Add(finishedTour);
        }

        public List<Tourist> GetAllTouristsByNames(string names)
        {
            var tourists = names.Split(", ").ToList();
            var retVal = new List<Tourist>();

            foreach (var tourist in tourists)
                retVal.Add(_touristService.GetByUsername(tourist));

            return retVal;
        }

        public List<Tourist> FinishedTourTourists(string tourists)
        {
            var finishedTourTourists = new List<Tourist>();

            foreach (var tourist in tourists.Split(", "))
                finishedTourTourists.Add(_touristService.GetByUsername(tourist));

            return finishedTourTourists;
        }

        public List<Location?> GetAllKeyPointsByIds(string keyPoints)
        {
            var locations = keyPoints.Split(", ").ToList();
            var retVal = new List<Location?>();

            foreach (var location in locations)
                retVal.Add(_locationService.GetById(int.Parse(location)));

            return retVal;
        }

        public Tour FindFinishedTourByName(string name)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from FinishedTour where Name = $Name";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Name", name);

            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read())
                return new Tour();

            return new Tour(selectReader.GetInt32(0), selectReader.GetString(1),
                GetAllKeyPointsByIds(selectReader.GetString(2)), GetAllTouristsByNames(selectReader.GetString(3)),
                selectReader.GetString(4));
        }

        public IChartValues GetAgeGroup(Tour finishedTour)
        {
            return _finishedTourRepository.GetAgeGroup(finishedTour);
        }


        public ChartValues<ObservableValue> GetVoucherOdds(Tour finishedTour)
        {
            var withVoucher = 0;
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            foreach (var tourist in finishedTour.Tourists)
            {
                const string selectStatement = "select * from TourVoucher where TouristId = $TouristId";
                using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
                selectCommand.Parameters.AddWithValue("$TouristId", tourist.Id);
                using var selectReader = selectCommand.ExecuteReader();
                if (!selectReader.Read()) continue;
                if ((TourVoucher.VoucherStatus)selectReader.GetInt32(6) == TourVoucher.VoucherStatus.Valid)
                    withVoucher++;
            }


            var voucherOdds = new ChartValues<ObservableValue> { new(withVoucher), new(finishedTour.Tourists.Count - withVoucher) };
            return voucherOdds;
        }

        public List<Location?> GetKeyPoints(string keyPoints)
        {
            var locations = keyPoints.Split(", ").ToList();
            var retVal = new List<Location?>();

            foreach (var location in locations)
                retVal.Add(_locationService.GetById(int.Parse(location)));

            return retVal;
        }

        public ObservableCollection<Tour> GetBestTour(SqliteCommand selectCommand)
        {
            using var selectReader = selectCommand.ExecuteReader();
            var finishedTours = new ObservableCollection<Tour>();
            while (selectReader.Read())
                finishedTours.Add(new Tour(selectReader.GetInt32(0), selectReader.GetString(1), 
                    GetKeyPoints(selectReader.GetString(2)), FinishedTourTourists(selectReader.GetString(3)), selectReader.GetString(4)));

            return finishedTours;
        }

        public ObservableCollection<Tour> GetAllTimeBestTour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from FinishedTour where TouristNumber = (select max(TouristNumber) from FinishedTour where GuideName = $GuideName)";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$GuideName", CurrentUser.Username);
            return GetBestTour(selectCommand);
        }

        public ObservableCollection<Tour> GetBestOf2022Tour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from FinishedTour where GuideName = $GuideName";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$GuideName", CurrentUser.Username);
            return _finishedTourRepository.FindBestTourByYear(GetBestTour(selectCommand), "2022");
        }

        public ObservableCollection<Tour> GetBestOf2023Tour()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from FinishedTour where GuideName = $GuideName";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$GuideName", CurrentUser.Username);
            return _finishedTourRepository.FindBestTourByYear(GetBestTour(selectCommand), "2023");
        }

        public string GetNewTourName()
        {
            return _finishedTourRepository.GetNewTourName();
        }
    }
}
