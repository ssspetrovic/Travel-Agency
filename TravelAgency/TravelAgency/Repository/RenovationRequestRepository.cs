using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class RenovationRequestRepository : RepositoryBase
    {
        public void Add(RenovationRequest renovationRequest)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into renovationrequest(comment, type, date, accId) values ($comment, $type, $date, $accid)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);

            insertCommand.Parameters.AddWithValue("$comment", renovationRequest.Comment);
            insertCommand.Parameters.AddWithValue("$type", renovationRequest.Type);
            insertCommand.Parameters.AddWithValue("$date", renovationRequest.Date);
            insertCommand.Parameters.AddWithValue("$accid", renovationRequest.AccId);

            insertCommand.ExecuteNonQuery();
        }
    }
}
