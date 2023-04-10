using System;
using System.Collections.ObjectModel;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class TourReservationRepository : RepositoryBase, ITourReservationRepository
    {
        public void Add(TourReservation tourReservation)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO TourReservation (TourId, TourName, GuestNumber, Username, TouristName)
                    VALUES ($TourId, $TourName, $GuestNumber, $Username, $TouristName)
                ";
            insertCommand.Parameters.AddWithValue("TourId", tourReservation.TourId);
            insertCommand.Parameters.AddWithValue("TourName", tourReservation.TourName);
            insertCommand.Parameters.AddWithValue("GuestNumber", tourReservation.GuestNumber);
            insertCommand.Parameters.AddWithValue("Username", tourReservation.TouristUsername);
            insertCommand.Parameters.AddWithValue("TouristName", tourReservation.TouristName);
            insertCommand.ExecuteNonQuery();
        }

        public void DeleteById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var deleteCommand = databaseConnection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM TourReservation WHERE Id = $Id";
            deleteCommand.Parameters.AddWithValue("$Id", id);
            deleteCommand.ExecuteNonQuery();
        }

        public Collection<TourReservation> GetAllAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM TourReservation WHERE Username = $CurrentUserUsername";
            selectCommand.Parameters.AddWithValue("$CurrentUserUsername", CurrentUser.Username);
            using var selectReader = selectCommand.ExecuteReader();

            var tourReservations = new Collection<TourReservation>();
            while (selectReader.Read())
            {
                tourReservations.Add(new TourReservation(
                    selectReader.GetInt32(0),
                    selectReader.GetInt32(1),
                    selectReader.GetString(2),
                    selectReader.GetInt32(3),
                    selectReader.GetString(4),
                    selectReader.GetString(5)
                ));
            }

            return tourReservations;
        }

        public TourReservation GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
