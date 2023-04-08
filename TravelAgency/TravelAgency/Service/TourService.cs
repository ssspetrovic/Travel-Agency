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

        public TourService()
        {
            _tourRepository = new TourRepository();
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

        public Tour GetByName(string? name)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            Tour? tour = null;

            const string selectStatement = "select * from Tour where Name = $Name";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Name", name);

            using var selectReader = selectCommand.ExecuteReader();

            if (!selectReader.Read()) return tour!;

            //Isto i ovde vazi, moramo izvrsiti konverziju
            var locationRepository = new LocationRepository();
            var location = locationRepository.GetById(selectReader.GetInt32(2));


            tour = new Tour(selectReader.GetInt32(0), selectReader.GetString(1),
                location!, selectReader.GetString(3), (Language)selectReader.GetInt32(4), selectReader.GetInt32(5),
                GetKeyPoints(selectReader.GetString(6)), selectReader.GetString(7), selectReader.GetFloat(8), selectReader.GetString(9));

            return tour;
        }

        public void Remove(int deletedTourId)
        {
            _tourRepository.Remove(deletedTourId);
        }

        public void RemoveDate(string cancelledDate, List<string> tourDates, int deletedTourId)
        {
            _tourRepository.RemoveDate(cancelledDate, tourDates, deletedTourId);
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


            //Posto nam je dosta parametara sacuvano u tabelu u vidu tekstova
            //Moramo da ih konvertujemo u liste podataka
            var locationRepository = new LocationRepository();
            var location = locationRepository.GetById(selectReader.GetInt32(2));

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

        public void UpdateMaxGuests(int selectedTourId, int selectedTourMaxGuests)
        {
            _tourRepository.UpdateMaxGuests(selectedTourId, selectedTourMaxGuests);
        }

        public DataTable GetAllAsDataTable(DataTable dt)
        {
            return _tourRepository.GetAllAsDataTable(dt);
        }

        public Language FindLanguage(string text)
        {
            return _tourRepository.FindLanguage(text);
        }

        public void Add(Tour tour)
        {
            _tourRepository.Add(tour);
        }
    }
}
