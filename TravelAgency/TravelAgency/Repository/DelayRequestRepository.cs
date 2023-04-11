using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<DelayRequest> GetDelayRequests(int ownerId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from DelayRequest";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var delayRequestList = new ObservableCollection<DelayRequest>();
            AccommodationRepository accommodationRepository = new AccommodationRepository();


            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var accId = selectReader.GetInt32(1);
                var userId = selectReader.GetInt32(2);
                var oldStartDate = selectReader.GetDateTime(3);
                var newStartDate = selectReader.GetDateTime(4);
                var oldEndDate = selectReader.GetDateTime(5);
                var newEndDate = selectReader.GetDateTime(6);

                //DateTime startDate = new DateTime(2023, 3, 15);
                //DateTime endDate = new DateTime(2023, 3, 19);

                int owner = accommodationRepository.GetOwnerIdByAccommodationId(ownerId);

                if (owner == ownerId)
                {
                    if (oldStartDate != newStartDate || oldEndDate != newEndDate)
                    {
                        DelayRequest del = new DelayRequest(accId, userId, oldStartDate, newStartDate, oldEndDate, newEndDate);
                        delayRequestList.Add(del);
                    }
                }
            }

            return delayRequestList;
        }
    }
}
