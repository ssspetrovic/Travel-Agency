using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class DelayRequestRepository : RepositoryBase, IDelayRequestRepository
    {
        public void AcceptDelayRequest(int reservationId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE DelayRequest SET status = 2
                    WHERE reservationId = $reservationId;
                ";
            updateCommand.Parameters.AddWithValue("$reservationId", reservationId);

            updateCommand.ExecuteNonQuery();
        }

        public void Add(DelayRequest delayRequest)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into DelayRequest(accId, userId, oldStartDate, newStartDate, oldEndDate, newEndDate, reservationId, status, comment) values ($accId, $userId, $oldStartDate, $newStartDate, $oldEndDate, $newEndDate, $reservationId, $status, $comment)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);

            insertCommand.Parameters.AddWithValue("$accId", delayRequest.AccommodationId);
            insertCommand.Parameters.AddWithValue("$userId", delayRequest.UserId);
            insertCommand.Parameters.AddWithValue("$oldStartDate", delayRequest.OldStartDate);
            insertCommand.Parameters.AddWithValue("$newStartDate", delayRequest.NewStartDate);
            insertCommand.Parameters.AddWithValue("$oldEndDate", delayRequest.OldEndDate);
            insertCommand.Parameters.AddWithValue("$newEndDate", delayRequest.NewEndDate);
            insertCommand.Parameters.AddWithValue("$reservationId", delayRequest.ReservationId);
            insertCommand.Parameters.AddWithValue("$status", delayRequest.RequestStatus);
            insertCommand.Parameters.AddWithValue("$comment", delayRequest.RejectionReason);
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<DelayRequest> GetAll()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from DelayRequest";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var delayRequestList = new ObservableCollection<DelayRequest>();


            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var accId = selectReader.GetInt32(1);
                var userId = selectReader.GetInt32(2);
                var oldStartDate = selectReader.GetDateTime(3);
                var newStartDate = selectReader.GetDateTime(4);
                var oldEndDate = selectReader.GetDateTime(5);
                var newEndDate = selectReader.GetDateTime(6);
                var reservationId = selectReader.GetInt32(7);
                var requestStatus = Enum.Parse<RequestStatusType>(selectReader.GetString(8));
                var rejectionReason = selectReader.GetString(9);
                DelayRequest del = new DelayRequest(reservationId, accId, userId, oldStartDate, newStartDate, oldEndDate, newEndDate, requestStatus, rejectionReason);
                delayRequestList.Add(del);
            }

            return delayRequestList;
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
                var reservationId = selectReader.GetInt32(7);
                var requestStatus = Enum.Parse<RequestStatusType>(selectReader.GetString(8));
                var rejectionReason = selectReader.GetString(9);

                int owner = accommodationRepository.GetOwnerIdByAccommodationId(ownerId);

                if (owner == ownerId)
                {
                    if (requestStatus == RequestStatusType.Processing)
                    {
                        DelayRequest del = new DelayRequest(reservationId, accId, userId, oldStartDate, newStartDate, oldEndDate, newEndDate, requestStatus, rejectionReason);
                        delayRequestList.Add(del);
                    }
                }
            }

            return delayRequestList;
        }

        public void RejectDelayRequest(int reservationId, string rejectionComment)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE DelayRequest SET status = 3, comment = $comment
                    WHERE reservationId = $reservationId;
                ";
            updateCommand.Parameters.AddWithValue("$reservationId", reservationId);
            updateCommand.Parameters.AddWithValue("$comment", rejectionComment);

            updateCommand.ExecuteNonQuery();
        }
    }
}
