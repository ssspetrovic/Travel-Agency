using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.Repository
{
    public class ReservationRepository : RepositoryBase, IReservationRepository
    {
        public void Add(Reservation reservation)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Reservation (Id, userId, accId, comment, startDate, endDate, gradeComplacent, gradeClean) 
                    values ($Id, $userId, $accId, $comment, $startDate, $endDate, $gradeComplacent, $gradeClean)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$Id", reservation.Id);
            insertCommand.Parameters.AddWithValue("$userId", reservation.GuestId);
            insertCommand.Parameters.AddWithValue("$accId", reservation.AccommodationId);
            insertCommand.Parameters.AddWithValue("$comment", reservation.UserComment);
            insertCommand.Parameters.AddWithValue("$startDate", reservation.StartDate);
            insertCommand.Parameters.AddWithValue("$endDate", reservation.EndDate);
            insertCommand.Parameters.AddWithValue("$gradeComplacent", reservation.GradeGuestComplacent);
            insertCommand.Parameters.AddWithValue("$gradeClean", reservation.GradeGuestClean);
            insertCommand.ExecuteNonQuery();
        }

        public void AddAutoId(Reservation reservation)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Reservation (userId, accId, userComment, startDate, endDate, gradeUserComplacent, gradeUserClean, reviewImages, gradeAccommodationClean, gradeAccommodationOwner, accommodationComment) 
                    values ($userId, $accId, $comment, $startDate, $endDate, $gradeComplacent, $gradeClean, $reviewImages, $gradeAccommodationClean, $gradeAccommodationOwner, $accommodationComment)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$userId", reservation.GuestId);
            insertCommand.Parameters.AddWithValue("$accId", reservation.AccommodationId);
            insertCommand.Parameters.AddWithValue("$comment", reservation.UserComment);
            insertCommand.Parameters.AddWithValue("$startDate", reservation.StartDate);
            insertCommand.Parameters.AddWithValue("$endDate", reservation.EndDate);
            insertCommand.Parameters.AddWithValue("$gradeComplacent", reservation.GradeGuestComplacent);
            insertCommand.Parameters.AddWithValue("$gradeClean", reservation.GradeGuestClean);
            insertCommand.Parameters.AddWithValue("$reviewImages", reservation.ReviewImagesURL);
            insertCommand.Parameters.AddWithValue("$gradeAccommodationClean", reservation.GradeAccommodationClean);
            insertCommand.Parameters.AddWithValue("$gradeAccommodationOwner", reservation.GradeAccommodationOwner);
            insertCommand.Parameters.AddWithValue("$accommodationComment", reservation.AccommodationComment);

            insertCommand.ExecuteNonQuery();
        }

        public int CountReservationsToGrade()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Reservation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            int count = 0;

            while (selectReader.Read())
            {
                //var startDate = selectReader.GetDateTime(4);
                //var endDate = selectReader.GetDateTime(5);
                var gradeComplacent = selectReader.GetFloat(6);
                var gradeClean = selectReader.GetFloat(7);

                DateTime startDate = new DateTime(2023, 3, 15);
                DateTime endDate = new DateTime(2023, 3, 19);
                //gradeClean = -1;
                //gradeComplacent = -1;

                if (gradeComplacent == -1 && gradeClean == -1)
                {
                    //DateTime endDate = DateTime.ParseExact(txtEndDate.Text, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    double days = (DateTime.Now - endDate).TotalDays;
                    if (days <= 5)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public ObservableCollection<Reservation> GetAll()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select Id, userId, accId, userComment, date(startDate), date(endDate), gradeUserComplacent, gradeUserClean, reviewImages, gradeAccommodationClean, gradeAccommodationOwner, accommodationComment from Reservation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var reservationList = new ObservableCollection<Reservation>();

            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var accommodationId = selectReader.GetInt32(2);
                var comment = selectReader.GetString(3);
                var startDate = selectReader.GetDateTime(4);
                var endDate = selectReader.GetDateTime(5);
                var gradeComplacent = selectReader.GetFloat(6);
                var gradeClean = selectReader.GetFloat(7);
                var reviewImages = selectReader.GetString(8);
                var gradeAccommodationClean = selectReader.GetFloat(9);
                var gradeAccommodationOwner = selectReader.GetFloat(10);
                var accommodationComment = selectReader.GetString(11);


                Reservation res = new Reservation(id, comment, startDate, endDate, gradeComplacent, gradeClean, guestId, accommodationId, accommodationComment, gradeAccommodationClean, gradeAccommodationOwner, reviewImages);

                reservationList.Add(res);
            }

            return reservationList;
        }

        public ObservableCollection<Reservation> GetReservationsToGrade()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Reservation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var reservationList = new ObservableCollection<Reservation>();


            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var accommodationId = selectReader.GetInt32(2);
                var comment = selectReader.GetString(3);

                var gradeComplacent = selectReader.GetFloat(6);
                var gradeClean = selectReader.GetFloat(7);

                DateTime startDate = new DateTime(2023, 3, 15);
                DateTime endDate = new DateTime(2023, 3, 19);

                if (gradeComplacent == -1 && gradeClean == -1)
                {
                    //DateTime endDate = DateTime.ParseExact(txtEndDate.Text, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    double days = (DateTime.Now - endDate).TotalDays;
                    if (days <= 5)
                    {
                        Reservation res = new Reservation(id, comment, startDate, endDate, gradeComplacent, gradeClean, guestId, accommodationId);
                        reservationList.Add(res);
                    }
                }
            }

            return reservationList;
        }

        public ObservableCollection<Reservation> GetAllByAccommodationId(int Id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select Id, userId, accId, userComment, date(startDate), date(endDate), gradeUserComplacent, gradeUserClean from Reservation where accId = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", Id);
            using var selectReader = selectCommand.ExecuteReader();

            var reservationList = new ObservableCollection<Reservation>();


            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var accommodationId = selectReader.GetInt32(2);
                var comment = selectReader.GetString(3);
                var startDate = selectReader.GetDateTime(4);
                var endDate = selectReader.GetDateTime(5);
                var gradeComplacent = selectReader.GetFloat(6);
                var gradeClean = selectReader.GetFloat(7);

                gradeClean = -1;
                gradeComplacent = -1;

                Reservation res = new Reservation(id, comment, startDate, endDate, gradeComplacent, gradeClean, guestId, accommodationId);
                reservationList.Add(res);
            }

            return reservationList;
        }

        public void UpdateReservationAfterGrading(int reservationId, string userComment, float gradeUserComplacent, float gradeUserClean)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE Reservation SET comment = $userComment, gradeUserComplacent = $gradeUserComplacent, gradeUserClean = $gradeUserClean
                    WHERE Id = $id;
                ";
            updateCommand.Parameters.AddWithValue("$comment", userComment);
            updateCommand.Parameters.AddWithValue("$gradeComplacent", gradeUserComplacent);
            updateCommand.Parameters.AddWithValue("$gradeClean", gradeUserClean);
            updateCommand.Parameters.AddWithValue("$id", reservationId);
            updateCommand.ExecuteNonQuery();
        }

        public void RemoveById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    DELETE from Reservation WHERE Id = $id
                ";
            updateCommand.Parameters.AddWithValue("$id", id);
            updateCommand.ExecuteNonQuery();
        }

        public ObservableCollection<Reservation> GetGuestsGradesToDisplay()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Reservation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var reservationList = new ObservableCollection<Reservation>();


            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var accommodationId = selectReader.GetInt32(2);
                var userComment = selectReader.GetString(3);
                var gradeUserComplacent = selectReader.GetInt32(6);
                var gradeUserClean = selectReader.GetInt32(7);
                var reviewImages = selectReader.GetString(8);
                var gradeAccommodationClean = selectReader.GetInt32(9);
                var gradeAccommodationOwner = selectReader.GetInt32(10);
                var accommodationComment = selectReader.GetString(11);

                DateTime startDate = new DateTime(2023, 3, 15);
                DateTime endDate = new DateTime(2023, 3, 19);

                if(id == 15)
                {
                    gradeUserClean = 2;
                    gradeUserComplacent = 5;
                    gradeAccommodationClean = 5;
                    gradeAccommodationOwner = 4;
                    accommodationComment = "Very clean and preaty";
                }

                if (gradeUserComplacent != -1 && gradeUserClean != -1)
                {
                    if (gradeAccommodationClean != -1 && gradeAccommodationOwner != -1)
                    {
                        Reservation res = new Reservation(id, userComment, startDate, endDate, gradeUserComplacent, gradeUserClean, guestId, accommodationId, accommodationComment, gradeAccommodationClean, gradeAccommodationOwner, reviewImages);
                        reservationList.Add(res);
                    }
                }
            }

            return reservationList;
        }

        public int CountGradesForOwner(int ownerId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Reservation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            int count = 0;
            AccommodationRepository accommodationRepository = new AccommodationRepository();

            while (selectReader.Read())
            {
                var accommodationId = selectReader.GetInt32(2);
                var tempOwnerId = accommodationRepository.GetOwnerIdByAccommodationId(accommodationId); 

                if(ownerId == tempOwnerId)
                {
                    var gradeAccommodationClean = selectReader.GetInt32(9);
                    var gradeAccommodationOwner = selectReader.GetInt32(10);

                    //## Test primer da bi se video ispis
                    if (selectReader.GetInt32(0) == 15)
                    {
                        gradeAccommodationClean = 5;
                        gradeAccommodationOwner = 5;
                    }
                    //##

                    if (gradeAccommodationClean != -1 && gradeAccommodationOwner != -1)
                        count++;
                }

            }

            return count;
        }

        public double AverageGradeForOwner(int ownerId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Reservation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            double average;
            int gradeSum = 0;
            AccommodationRepository accommodationRepository = new AccommodationRepository();

            while (selectReader.Read())
            {
                var accommodationId = selectReader.GetInt32(2);
                var tempOwnerId = accommodationRepository.GetOwnerIdByAccommodationId(accommodationId);

                if (ownerId == tempOwnerId)
                {
                    var gradeAccommodationClean = selectReader.GetInt32(9);
                    var gradeAccommodationOwner = selectReader.GetInt32(10);

                    //## Test primer da bi se video ispis
                    if(selectReader.GetInt32(0) == 15)
                    {
                        gradeAccommodationClean = 5;
                        gradeAccommodationOwner = 5;
                    }
                    //##

                    if(gradeAccommodationClean != -1 && gradeAccommodationOwner != -1)
                        gradeSum += gradeAccommodationClean + gradeAccommodationOwner;
                }
            }
            int count = CountGradesForOwner(ownerId);
            average = gradeSum / count;

            return average;
        }

        public void UpdateAfterRatingAccommodation(int id, string comment, string pictureUrl, int gradeClean, int gradeOwner)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE Reservation SET accommodationComment = $Comment, gradeAccommodationClean = $gradeAccommodationClean, gradeAccommodationOwner = $gradeAccommodationOwner, reviewImages = $url
                    WHERE Id = $id;
                ";
            updateCommand.Parameters.AddWithValue("$Comment", comment);
            updateCommand.Parameters.AddWithValue("$gradeAccommodationClean", gradeClean);
            updateCommand.Parameters.AddWithValue("$gradeAccommodationOwner", gradeOwner);
            updateCommand.Parameters.AddWithValue("$url", pictureUrl);
            updateCommand.Parameters.AddWithValue("$id", id);

            updateCommand.ExecuteNonQuery();
        }

        public void AcceptReservationChangeRequest(int reservationId, DateTime newStartDate, DateTime newEndDate)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE Reservation SET startDate = $startDate, endDate = $endDate
                    WHERE Id = $id;
                ";
            updateCommand.Parameters.AddWithValue("$startDate", newStartDate);
            updateCommand.Parameters.AddWithValue("$endDate", newEndDate);
            updateCommand.Parameters.AddWithValue("$id", reservationId);
            updateCommand.ExecuteNonQuery();
        }
    }
}
