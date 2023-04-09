using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class TourService : RepositoryBase
    {
        private readonly TourRepository _tourRepository;
        private readonly LocationService _locationService;

        public TourService()
        {
            _tourRepository = new TourRepository();
            _locationService = new LocationService();
        }

        public List<Location?> GetKeyPoints(string keyPoints)
        {
            var locations = keyPoints.Split(", ").ToList();
            var retVal = new List<Location?>();

            foreach (var location in locations)
                retVal.Add(_locationService.GetById(int.Parse(location)));

            return retVal;
        }

        public Tour GetByName(string? name)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from Tour where Name = $Name";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Name", name);

            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read()) return new Tour();
            var location = _locationService.GetById(selectReader.GetInt32(2));


            return new Tour(selectReader.GetInt32(0), selectReader.GetString(1),
                location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                GetKeyPoints(selectReader.GetString(6)), selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9));

        }

        public void Remove(int id)
        {
            _tourRepository.Remove(id);
        }

        public void RemoveDate(string date, List<string> allDates, int id)
        {
            _tourRepository.RemoveDate(date, allDates, id);
        }

        public Tour GetById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from Tour where Id = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", id);

            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read()) return new Tour();
            var location = _locationService.GetById(selectReader.GetInt32(2));

            var tour = new Tour(selectReader.GetInt32(0), selectReader.GetString(1),
                location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                GetKeyPoints(selectReader.GetString(6)), selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9));

            return tour;
        }

        public ObservableCollection<Tour> GetAllAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Tour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var tourList = new ObservableCollection<Tour>();

            while (selectReader.Read())
            {
                var location = _locationService.GetById(selectReader.GetInt32(2));

                tourList.Add(new Tour(selectReader.GetInt32(0), selectReader.GetString(1),
                    location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                    GetKeyPoints(selectReader.GetString(6)), selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9)));
            }

            return tourList;
        }

        public void UpdateMaxGuests(int id, int maxGuests)
        {
            _tourRepository.UpdateMaxGuests(id, maxGuests);
        }

        public DataTable GetAllAsDataTable(DataTable dataTable)
        {
            return _tourRepository.GetAllAsDataTable(dataTable);
        }

        public Language FindLanguage(string language)
        {
            return _tourRepository.FindLanguage(language);
        }

        public void Add(Tour tour)
        {
            _tourRepository.Add(tour);
        }
    }
}
