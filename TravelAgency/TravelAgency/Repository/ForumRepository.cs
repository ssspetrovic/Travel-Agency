using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelAgency.Model;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace TravelAgency.Repository
{
    internal class ForumRepository : RepositoryBase
    {
        public ForumRepository()
        {
        }

        public void Add(Forum forum)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Forum(guestId, locationId, guestCommentNumber, ownerCommentNumber, isClosed) 
                    values ($guestId, $locationId, $guestCommentNumber, $ownerCommentNumber, $isClosed)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$guestId", forum.GuestId);
            insertCommand.Parameters.AddWithValue("$locationId", forum.LocationId);
            insertCommand.Parameters.AddWithValue("$guestCommentNumber", forum.GuestCommentNumber);
            insertCommand.Parameters.AddWithValue("$ownerCommentNumber", forum.OwnerCommentNumber);
            insertCommand.Parameters.AddWithValue("$isClosed", forum.IsClosed);
            insertCommand.ExecuteNonQuery();
        }

        public void Update(Forum forum)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var updateClosed = databaseConnection.CreateCommand();
            var updateGuestCommentNum = databaseConnection.CreateCommand();
            var updateOwnerCommentNum = databaseConnection.CreateCommand();
            updateClosed.CommandText =
                @"
                    update Forum set isClosed = $value where Id = $Id;
                ";
            updateGuestCommentNum.CommandText =
                @"
                    update Forum set guestCommentNumber = $value where Id = $Id;
                ";
            updateOwnerCommentNum.CommandText =
                @"
                    update Forum set ownerCommentNumber = $value where Id = $Id;
                ";

            updateClosed.Parameters.AddWithValue("$value", forum.IsClosed);
            updateClosed.Parameters.AddWithValue("$Id", forum.Id);

            updateGuestCommentNum.Parameters.AddWithValue("$value", forum.GuestCommentNumber);
            updateGuestCommentNum.Parameters.AddWithValue("$Id", forum.Id);

            updateOwnerCommentNum.Parameters.AddWithValue("$value", forum.OwnerCommentNumber);
            updateOwnerCommentNum.Parameters.AddWithValue("$Id", forum.Id);

            updateClosed.ExecuteNonQuery();
            updateGuestCommentNum.ExecuteNonQuery();
            updateOwnerCommentNum.ExecuteNonQuery();
        }

        public void UpdateVisitedByOwner(int forumId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var updateClosed = databaseConnection.CreateCommand();
            updateClosed.CommandText =
                @"
                    update Forum set visitedByOwner = $value where Id = $forumId;
                ";

            updateClosed.Parameters.AddWithValue("$value", 1);
            updateClosed.Parameters.AddWithValue("$forumId", forumId);

            updateClosed.ExecuteNonQuery();
        }

        public void UpdateOwnerCommentNumber(int forumId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var updateClosed = databaseConnection.CreateCommand();
            updateClosed.CommandText =
                @"
                    update Forum set ownerCommentNumber = ownerCommentNumber + 1 where Id = $forumId;
                ";

            updateClosed.Parameters.AddWithValue("$forumId", forumId);

            updateClosed.ExecuteNonQuery();
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
                commentList.Add(new Comment(selectReader.GetInt32(0), selectReader.GetInt32(1), selectReader.GetInt32(2), selectReader.GetString(3), type, selectReader.GetDateTime(5)));
            }

            return commentList;
        }

        public ObservableCollection<Forum> GetByLocationId(int Id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Forum where locationId = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", Id);
            using var selectReader = selectCommand.ExecuteReader();

            var forumList = new ObservableCollection<Forum>();

            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var locationId = selectReader.GetInt32(2);
                var guestCommentNumber = selectReader.GetInt32(3);
                var ownerCommentNumber = selectReader.GetInt32(4);
                var isClosed = selectReader.GetBoolean(5);
                var visitedByOwner = selectReader.GetInt32(6);

                Forum forum = new Forum(id, guestId, isClosed, guestCommentNumber, ownerCommentNumber, locationId, visitedByOwner);

                forumList.Add(forum);
            }

            return forumList;
        }

        public ObservableCollection<Forum> GetAll()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Forum";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var forumList = new ObservableCollection<Forum>();

            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var locationId = selectReader.GetInt32(2);
                var guestCommentNumber = selectReader.GetInt32(3);
                var ownerCommentNumber = selectReader.GetInt32(4);
                var isClosed = selectReader.GetBoolean(5);
                var visitedByOwner = selectReader.GetInt32(6);

                Forum forum = new Forum(id, guestId, isClosed, guestCommentNumber, ownerCommentNumber, locationId, visitedByOwner);

                forumList.Add(forum);
            }

            return forumList;
        }
    }
}
