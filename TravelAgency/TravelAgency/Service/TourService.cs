using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using iTextSharp.text;
using TravelAgency.Interface;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class TourService : RepositoryBase
    {
        private readonly ITourRepository _tourRepository;
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
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Tour(Name, Location_Id, Description, Language, MaxGuests, LocationList, Date, Duration, Images) 
                    values ($Name, $Location_Id, $Description, $Language, $MaxGuests, $LocationList, $Date, $Duration, $Images)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$Name", tour.Name);
            insertCommand.Parameters.AddWithValue("$Location_Id", tour.Location.Id);
            insertCommand.Parameters.AddWithValue("$Description", tour.Description);
            insertCommand.Parameters.AddWithValue("$Language", tour.Language);
            insertCommand.Parameters.AddWithValue("$MaxGuests", tour.MaxGuests);
            insertCommand.Parameters.AddWithValue("$LocationList", _tourRepository.GetIdList(tour));
            insertCommand.Parameters.AddWithValue("Date", tour.Date);
            insertCommand.Parameters.AddWithValue("$Duration", tour.Duration);
            insertCommand.Parameters.AddWithValue("$Images", tour.Photos);
            insertCommand.ExecuteNonQuery();
        }

        public bool CheckIfDatesExist(string dates)
        {
            return _tourRepository.CheckIfDatesExist(dates);
        }

        public List<Paragraph> CreateDocumentData(DateTime startDate, DateTime endDate)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var documentData = new List<Paragraph>();

            const string selectStatement = "select * from Tour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
            {
                foreach (var date in selectReader.GetString(7).Split(","))
                {
                    var currentDate = DateTime.Parse(date);
                    if (currentDate <= startDate || currentDate >= endDate) continue;

                    var startingLocation = _locationService.GetById(selectReader.GetInt32(2))?.City;
                    var keyPoints = GetKeyPoints(selectReader.GetString(6))
                        .Select(location => $"{location!.City}, {location.Country}")
                        .DefaultIfEmpty("No Key Points!")
                        .Aggregate((current, next) => current + "; " + next);

                    var data = new Paragraph($"Tour: {selectReader.GetString(1)}\nStarting Location: {startingLocation}\nKeyPoints: {keyPoints}\nLanguage: " +
                                             $"{(Language)selectReader.GetInt32(4)}\nMaximum Number of Guests: {selectReader.GetInt32(5)}\n \n", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                    documentData.Add(data);
                    break;
                }
            }

            return documentData;
        }

        public string GetFormattedKeyPoints(IEnumerable<Location?> keyPoints)
        {
            var locationService = new LocationService();
            return keyPoints.Aggregate(string.Empty, (current, location) => current + " - " + locationService.GetById(location!.Id)! + "\r\n");
        }

        public List<Uri> ParsePhotosUris(string photosString)
        {
            var uriStrings = photosString.Split(", ");
            return uriStrings.Select(uriString => new Uri(uriString)).ToList();
        }
    }
}
