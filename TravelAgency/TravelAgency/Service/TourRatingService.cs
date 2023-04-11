using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class TourRatingService : RepositoryBase
    {
        private readonly TourRatingRepository _tourRatingRepository;
        private readonly TouristService _touristService;
        private readonly LocationService _locationService;
        private readonly TourService _tourService;

        public TourRatingService()
        {
            _tourRatingRepository = new TourRatingRepository();
            _touristService = new TouristService();
            _locationService = new LocationService();
            _tourService = new TourService();
        }

        public void Add(TourRating tourRating)
        {
            _tourRatingRepository.Add(tourRating);
        }

        public List<int> GetTouristIds(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from TourRating where TourId = $TourId";
            var ids = new List<int>();

            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$TourId", id);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
                ids.Add(selectReader.GetInt32(1));

            return ids;
        }

        public List<string> GetTouristsByTourId(int id)
        {
            var touristIds = GetTouristIds(id);
            var tourists = new List<Tourist>();

            foreach(var touristId in touristIds)
                tourists.Add(_touristService.GetById(touristId));

            var retVal = new List<string>();

            foreach(var tourist in tourists)
                retVal.Add(tourist.Name + ", " + _locationService.GetById(tourist.LocationId) + ", " + tourist.TouristAppearance);

            return retVal;
        }

        public List<string> GetCommentsByTourId(int id)
        {
            return _tourRatingRepository.GetCommentsByTourId(id);
        }

        public List<string> GetRatingsByTourId(int id)
        {
            return _tourRatingRepository.GetRatingsByTourId(id);
        }

        public void ReportAComment(string comment)
        {
            _tourRatingRepository.ReportAComment(comment);
        }

        private Tour GetTourByTouristId(string touristName)
        {
            var touristId = _touristService.GetByUsername(touristName).Id;

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from TourRating where TouristId = $TouristId";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$TouristId", touristId);

            using var selectReader = selectCommand.ExecuteReader();

            return !selectReader.Read() ? new Tour() : _tourService.GetById(selectReader.GetInt32(2));
        }

        public void ReportValidation(string comment, string tourist)
        {
            if (!tourist.Contains("Present"))
                ReportAComment(comment);
            var touristName = tourist.Split(", ")[0];

            var tour = GetTourByTouristId(touristName);
            foreach (var keyPoint in tour.KeyPoints)
                if (keyPoint != null && _touristService.GetByUsername(touristName).LocationId == keyPoint.Id)
                    break;
                else if (comment.Contains(keyPoint!.City) || comment.Contains(keyPoint.Country))
                    ReportAComment(comment);
        }

        public bool IsTourRateable(string? touristUsername, string? tourName)
        {
            if (_touristService.GetTouristAppearance(touristUsername) != TouristAppearance.Present)
                return false;

            var finishedTourService = new FinishedTourService();
            return !finishedTourService.CheckExistingTours(_tourService.GetByName(tourName));
        }
    }
}
