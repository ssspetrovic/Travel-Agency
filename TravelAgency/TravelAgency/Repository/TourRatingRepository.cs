using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace TravelAgency.Repository
{
    internal class TourRatingRepository : RepositoryBase
    {
        public List<string> GetCommentsByTourId(int id)
        {
            using var databaseConnection = GetConnection();
            const string selectStatement = "select * from TourRating where TourId = $TourId";
            var comments = new List<string>();
            
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$TourId", id);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
                comments.Add(selectReader.GetString(6));

            return comments;
        }

        public List<double> GetRatingsByTourId(int id)
        {
            using var databaseConnection = GetConnection();
            const string selectStatement = "select * from TourRating where TourId = $TourId";
            var ratings = new List<double>();

            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$TourId", id);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
                ratings.Add((double)(selectReader.GetInt32(3) + selectReader.GetInt32(4) + selectReader.GetInt32(5))/3);

            return ratings;
        }
    }
}
