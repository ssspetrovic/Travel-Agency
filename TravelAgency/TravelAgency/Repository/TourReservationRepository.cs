﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            insertCommand.Parameters.AddWithValue("Username", tourReservation.Username);
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

        public TourReservation GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}