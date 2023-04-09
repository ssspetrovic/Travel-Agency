using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace TravelAgency.Repository
{
    internal class TourRatingRepository : RepositoryBase
    {
        public List<string> GetCommentsByTourId(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from TourRating where TourId = $TourId";
            var comments = new List<string>();
            
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$TourId", id);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
                comments.Add(selectReader.GetString(6));

            return comments;
        }

        public List<string> AddFlaggedRatings(SqliteDataReader selectReader)
        {
            var ratings = new List<double>();
            var flags = new List<string>();

            while (selectReader.Read())
            {
                var rating = (double)(selectReader.GetInt32(3) + selectReader.GetInt32(4) + selectReader.GetInt32(5)) / 3;
                ratings.Add(rating);

                var flag = selectReader.GetInt32(8) == 1 ? "⚠️" : "";
                flags.Add(flag);
            }

            return flags.Zip(ratings, (flag, rating) => flag == "⚠️" ? flag : $"{Math.Round(rating, 2)}").ToList();
        }




        public List<string> GetRatingsByTourId(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from TourRating where TourId = $TourId";

            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$TourId", id);
            using var selectReader = selectCommand.ExecuteReader();

            var ratingsWithFlags = AddFlaggedRatings(selectReader);

            return ratingsWithFlags;
        }

        public void ReportAComment(string comment)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string updateStatement = "update TourRating set IsCommentReported = 1 where Comment = $Comment";
            var updateCommand = new SqliteCommand(updateStatement, databaseConnection);

            updateCommand.Parameters.AddWithValue("$Comment", comment);
            updateCommand.ExecuteNonQuery();
        }
    }
}
