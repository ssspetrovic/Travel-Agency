using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.Repository
{
    internal class CommentRepository: RepositoryBase
    {
        public CommentRepository() { }
        public void Add(Comment comment)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Comment(guestId, forumId, text, date, type) 
                    values ($guestId, $forumId, $text, $date, $type)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$guestId", comment.GuestId);
            insertCommand.Parameters.AddWithValue("$forumId", comment.ForumId);
            insertCommand.Parameters.AddWithValue("$text", comment.Text);
            insertCommand.Parameters.AddWithValue("$date", comment.Date);
            insertCommand.Parameters.AddWithValue("$type", comment.CommentType.ToString());
            insertCommand.ExecuteNonQuery();
        }

        public ObservableCollection<Comment> GetByForumId(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select Id, guestId, forumId, text, type, date(date) from Comment where forumId = $id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$id", id);
            using var selectReader = selectCommand.ExecuteReader();

            var commentList = new ObservableCollection<Comment>();


            while (selectReader.Read())
            {
                var type = Enum.Parse<CommentType>(selectReader.GetString(4));
                commentList.Add(new Comment(selectReader.GetInt32(0), selectReader.GetInt32(1), selectReader.GetInt32(2), selectReader.GetString(3),type , selectReader.GetDateTime(5)));
            }

            return commentList;
        }
    }
}
