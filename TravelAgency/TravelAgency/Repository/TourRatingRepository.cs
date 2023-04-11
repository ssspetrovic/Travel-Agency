using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class TourRatingRepository : RepositoryBase
    {
        public void Add(TourRating tourRating)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO TourRating (TouristId, TourId, GuideKnowledgeGrade, GuideLanguageGrade, TourInterestingnessGrade, Comment, Photos)
                    VALUES ($TouristId, $TourId, $GuideKnowledgeGrade, $GuideLanguageGrade, $TourInterestingnessGrade, $Comment, $Photos)
                ";

            insertCommand.Parameters.AddWithValue("$TouristId", tourRating.TouristId);
            insertCommand.Parameters.AddWithValue("$TourId", tourRating.TourId);
            insertCommand.Parameters.AddWithValue("$GuideKnowledgeGrade", (int)tourRating.GuideKnowledgeGrade);
            insertCommand.Parameters.AddWithValue("$GuideLanguageGrade", (int)tourRating.GuideLanguageGrade);
            insertCommand.Parameters.AddWithValue("$TourInterestingnessGrade", (int)tourRating.TourInterestingnessGrade);
            insertCommand.Parameters.AddWithValue("$Comment", tourRating.Comment);
            insertCommand.Parameters.AddWithValue("$Photos", tourRating.Photos);

            insertCommand.ExecuteNonQuery();
        }

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
