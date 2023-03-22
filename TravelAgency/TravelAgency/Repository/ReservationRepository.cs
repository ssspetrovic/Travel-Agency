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
            insertCommand.Parameters.AddWithValue("$comment", reservation.Comment);
            insertCommand.Parameters.AddWithValue("$startDate", reservation.StartDate);
            insertCommand.Parameters.AddWithValue("$endDate", reservation.EndDate);
            insertCommand.Parameters.AddWithValue("$gradeComplacent", reservation.GradeComplaisent);
            insertCommand.Parameters.AddWithValue("$gradeClean", reservation.GradeClean);
            insertCommand.ExecuteNonQuery();
        }

        public void AddAutoId(Reservation reservation)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Reservation (userId, accId, comment, startDate, endDate, gradeComplacent, gradeClean) 
                    values ($userId, $accId, $comment, $startDate, $endDate, $gradeComplacent, $gradeClean)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$userId", reservation.GuestId);
            insertCommand.Parameters.AddWithValue("$accId", reservation.AccommodationId);
            insertCommand.Parameters.AddWithValue("$comment", reservation.Comment);
            insertCommand.Parameters.AddWithValue("$startDate", reservation.StartDate);
            insertCommand.Parameters.AddWithValue("$endDate", reservation.EndDate);
            insertCommand.Parameters.AddWithValue("$gradeComplacent", reservation.GradeComplaisent);
            insertCommand.Parameters.AddWithValue("$gradeClean", reservation.GradeClean);
            insertCommand.ExecuteNonQuery();
        }

        public int CountReservationsToGrade()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Reservation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var reservationList = new ObservableCollection<Reservation>();
            var locationRepository = new LocationRepository();

            int count = 0;

            while (selectReader.Read())
            {
                var location = locationRepository.GetById(selectReader.GetInt32(2));
                var keyPointsList = selectReader.GetString(6).Split(", ");
                var keyPoints = locationRepository.GetByAllCities(keyPointsList.ToList());

                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var accommodationId = selectReader.GetInt32(2);
                var comment = selectReader.GetString(3);
                //var startDate = selectReader.GetDateTime(4);
                //var endDate = selectReader.GetDateTime(5);
                var gradeComplaisent = selectReader.GetFloat(6);
                var gradeClean = selectReader.GetFloat(7);

                DateTime startDate = new DateTime(2023, 3, 15);
                DateTime endDate = new DateTime(2023, 3, 19);
                gradeClean = -1;
                gradeComplaisent = -1;

                if (gradeComplaisent == -1 && gradeClean == -1)
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

            const string selectStatement = @"select * from Reservation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var reservationList = new ObservableCollection<Reservation>();
            var locationRepository = new LocationRepository();


            while (selectReader.Read())
            {
                var location = locationRepository.GetById(selectReader.GetInt32(2));
                var keyPointsList = selectReader.GetString(6).Split(", ");
                var keyPoints = locationRepository.GetByAllCities(keyPointsList.ToList());

                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var accommodationId = selectReader.GetInt32(2);
                var comment = selectReader.GetString(3);
                //var startDate = selectReader.GetDateTime(4);
                //var endDate = selectReader.GetDateTime(5);
                var gradeComplaisent = selectReader.GetFloat(6);
                var gradeClean = selectReader.GetFloat(7);

                DateTime startDate = new DateTime(2023, 3, 15);
                DateTime endDate = new DateTime(2023, 3, 19);

                Reservation res = new Reservation(id, comment, startDate, endDate, gradeComplaisent, gradeClean, guestId, accommodationId);

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
            var locationRepository = new LocationRepository();


            while (selectReader.Read())
            {
                var location = locationRepository.GetById(selectReader.GetInt32(2));
                var keyPointsList = selectReader.GetString(6).Split(", ");
                var keyPoints = locationRepository.GetByAllCities(keyPointsList.ToList());

                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var accommodationId = selectReader.GetInt32(2);
                var comment = selectReader.GetString(3);
                //var startDate = selectReader.GetDateTime(4);
                //var endDate = selectReader.GetDateTime(5);
                var gradeComplaisent = selectReader.GetFloat(6);
                var gradeClean = selectReader.GetFloat(7);

                DateTime startDate = new DateTime(2023, 3, 15);
                DateTime endDate = new DateTime(2023, 3, 19);
                gradeClean = -1;
                gradeComplaisent = -1;

                if (gradeComplaisent == -1 && gradeClean == -1)
                {
                    //DateTime endDate = DateTime.ParseExact(txtEndDate.Text, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    double days = (DateTime.Now - endDate).TotalDays;
                    if (days <= 5)
                    {
                        Reservation res = new Reservation(id, comment, startDate, endDate, gradeComplaisent, gradeClean, guestId, accommodationId);
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

            const string selectStatement = @"select Id, userId, accId, comment, date(startDate), date(endDate), gradeComplacent, gradeClean from Reservation where accId = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", Id);
            using var selectReader = selectCommand.ExecuteReader();

            var reservationList = new ObservableCollection<Reservation>();
            var locationRepository = new LocationRepository();


            while (selectReader.Read())
            {
                var location = locationRepository.GetById(selectReader.GetInt32(2));
                var keyPointsList = selectReader.GetString(6).Split(", ");
                var keyPoints = locationRepository.GetByAllCities(keyPointsList.ToList());

                var id = selectReader.GetInt32(0);
                var guestId = selectReader.GetInt32(1);
                var accommodationId = selectReader.GetInt32(2);
                var comment = selectReader.GetString(3);
                var startDate = selectReader.GetDateTime(4);
                var endDate = selectReader.GetDateTime(5);
                var gradeComplaisent = selectReader.GetFloat(6);
                var gradeClean = selectReader.GetFloat(7);

                gradeClean = -1;
                gradeComplaisent = -1;

                Reservation res = new Reservation(id, comment, startDate, endDate, gradeComplaisent, gradeClean, guestId, accommodationId);
                reservationList.Add(res);
            }

            return reservationList;
        }

        public void UpdateReservationAfterGrading(int reservationId, string comment, float gradeComplaisent, float gradeClean)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE Reservation SET comment = $comment, gradeComplacent = $gradeComplacent, gradeClean = $gradeClean
                    WHERE Id = $id;
                ";
            updateCommand.Parameters.AddWithValue("$comment", comment);
            updateCommand.Parameters.AddWithValue("$gradeComplacent", gradeComplaisent);
            updateCommand.Parameters.AddWithValue("$gradeClean", gradeClean);
            updateCommand.Parameters.AddWithValue("$id", reservationId);
            updateCommand.ExecuteNonQuery();
        }
    }
}
