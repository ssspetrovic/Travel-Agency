using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    public interface ITourRatingRepository
    {
        void Add(TourRating tourRating);
        List<string> GetCommentsByTourId(int id);
        List<string> AddFlaggedRatings(SqliteDataReader selectReader);
        List<string> GetRatingsByTourId(int id);
        void ReportAComment(string comment);
    }
}
