﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.DTO;
using TravelAgency.Service;
using System.Diagnostics;

namespace TravelAgency.Repository
{
    public class AccommodationRepository : RepositoryBase, IAccommodationRepository
    {
        public void Add(Accommodation accommodation)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Accommodation(name, type, maxGuest, minGuest, locationId, adress, reservableDays, images, description, ownerId) 
                    values ($name, $type, $maxGuest, $minGuest, $locationId, $adress, $reservableDays, $images, $description, $ownerId)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$name", accommodation.Name);
            insertCommand.Parameters.AddWithValue("$type", accommodation.Type);
            insertCommand.Parameters.AddWithValue("$maxGuest", accommodation.MaxGuest);
            insertCommand.Parameters.AddWithValue("$minGuest", accommodation.MinGuest);
            insertCommand.Parameters.AddWithValue("$locationId", accommodation.LocationId);
            insertCommand.Parameters.AddWithValue("$adress", accommodation.Address);
            insertCommand.Parameters.AddWithValue("$reservableDays", accommodation.ReservableDays);
            insertCommand.Parameters.AddWithValue("$images", accommodation.Images);
            insertCommand.Parameters.AddWithValue("$description", accommodation.Description);
            insertCommand.Parameters.AddWithValue("$ownerId", accommodation.OwnerId);
            insertCommand.ExecuteNonQuery();
        }

        public void Edit(Accommodation accommodation)
        {
            throw new NotImplementedException();
        }

        public Language FindLanguage(string language)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<AccommodationDTO> GetAll()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Accommodation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var accommodationList = new ObservableCollection<AccommodationDTO>();
            var locationService = new LocationService();


            while (selectReader.Read())
            {
                var location = locationService.GetById(selectReader.GetInt32(5));
                var type = Enum.Parse<AccommodationType>(selectReader.GetString(2));
                System.Diagnostics.Debug.WriteLine(selectReader.GetString(7) + "...");
               accommodationList.Add(new AccommodationDTO(selectReader.GetInt32(0), selectReader.GetString(1), location, type, selectReader.GetInt32(3), selectReader.GetInt32(4), selectReader.GetInt32(7), selectReader.GetString(8), selectReader.GetInt32(11)));
            }

            return accommodationList;
        }


        public ObservableCollection<AccommodationDTO> GetAllByOwnerId()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Accommodation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var accommodationList = new ObservableCollection<AccommodationDTO>();
            var locationService = new LocationService();


            while (selectReader.Read())
            {
                var ownerId = selectReader.GetInt32(10);
                if (ownerId == CurrentUser.Id)
                {
                    var location = locationService.GetById(selectReader.GetInt32(5));
                    var type = Enum.Parse<AccommodationType>(selectReader.GetString(2));
                    System.Diagnostics.Debug.WriteLine(selectReader.GetString(7) + "...");
                    accommodationList.Add(new AccommodationDTO(selectReader.GetInt32(0), selectReader.GetString(1), location, type, selectReader.GetInt32(3), selectReader.GetInt32(4), selectReader.GetInt32(7), selectReader.GetString(8), selectReader.GetInt32(11)));
                }
            }

            return accommodationList;
        }

        public DataTable GetByAll(DataTable dt)
        {
            throw new NotImplementedException();
        }

        public AccommodationDTO GetById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var locationService = new LocationService();

            const string selectStatement = @"select * from Accommodation where Id = $id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$id", id);
            using var selectReader = selectCommand.ExecuteReader();

            if (selectReader.Read()) {
                System.Diagnostics.Debug.WriteLine(selectCommand.ToString());
                var location = locationService.GetById(selectReader.GetInt32(5));
                var type = Enum.Parse<AccommodationType>(selectReader.GetString(2));
                return new AccommodationDTO(selectReader.GetInt32(0), selectReader.GetString(1), location, type, selectReader.GetInt32(3), selectReader.GetInt32(4), selectReader.GetInt32(7), selectReader.GetString(8), selectReader.GetInt32(11));
            }
            return null;
        }

        public AccommodationDTO GetByName(string name)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var locationService = new LocationService();

            const string selectStatement = @"select * from Accommodation where name = $name";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$name", name);
            using var selectReader = selectCommand.ExecuteReader();

            if (selectReader.Read())
            {
                System.Diagnostics.Debug.WriteLine(selectCommand.ToString());
                var location = locationService.GetById(selectReader.GetInt32(5));
                var type = Enum.Parse<AccommodationType>(selectReader.GetString(2));
                return new AccommodationDTO(selectReader.GetInt32(0), selectReader.GetString(1), location, type, selectReader.GetInt32(3), selectReader.GetInt32(4), selectReader.GetInt32(7), selectReader.GetString(8), selectReader.GetInt32(11));
            }
            return null;
        }

        public int GetOwnerIdByAccommodationId(int accommodationId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select ownerId from Accommodation where Id = $id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$id", accommodationId);
            using var selectReader = selectCommand.ExecuteReader();
            int ownerId = 0;

            if (selectReader.Read())
            {
                System.Diagnostics.Debug.WriteLine(selectCommand.ToString());
                ownerId = selectReader.GetInt32(0);

            }
            return ownerId;
        }

        public void RemoveByName(string accName)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE Accommodation SET removed = 1
                    WHERE name = '$name';
                ";
            updateCommand.Parameters.AddWithValue("$name", accName);
            updateCommand.ExecuteNonQuery();
        }
    }
}
