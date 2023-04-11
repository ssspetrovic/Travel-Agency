using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class DelayRequestRepository : RepositoryBase, IDelayRequestRepository
    {
        public void Add(DelayRequest delayRequest)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into DelayRequest(accId, userId, oldStartDate, newStartDate, oldEndDate, newEndDate) values ($accId, $userId, $oldStartDate, $newStartDate, $oldEndDate, $newEndDate)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);

            insertCommand.Parameters.AddWithValue("$accId", delayRequest.AccommodationId);
            insertCommand.Parameters.AddWithValue("$userId", delayRequest.UserId);
            insertCommand.Parameters.AddWithValue("$oldStartDate", delayRequest.OldStartDate);
            insertCommand.Parameters.AddWithValue("$newStartDate", delayRequest.NewStartDate);
            insertCommand.Parameters.AddWithValue("$oldEndDate", delayRequest.OldEndDate);
            insertCommand.Parameters.AddWithValue("$newEndDate", delayRequest.NewEndDate);
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
