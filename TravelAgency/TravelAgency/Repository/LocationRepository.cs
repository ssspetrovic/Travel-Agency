﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class LocationRepository : RepositoryBase, ILocationRepository
    {
        private const string DatabaseFilePath = "../../../Resources/Data/data.db";

        public void Add(LocationModel locationModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(LocationModel locationModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public LocationModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public LocationModel? GetByCity(string city)
        {
            using var databaseConnection = new SqliteConnection("Data Source=" + DatabaseFilePath);
            databaseConnection.Open();

            const string selectStatement = @"select * from Location where City = $City ";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$City", city);
            using var selectReader = selectCommand.ExecuteReader();

            if (selectReader.Read())
            {
                return new LocationModel(selectReader.GetInt32(0), selectReader.GetString(1),
                    selectReader.GetString(2));
            }

            return null;
        }

        public IEnumerable<LocationModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public List<LocationModel> GetByAllCities(List<string> cities)
        {
            var locations = new List<LocationModel>();
            if (locations == null) 
                throw new ArgumentNullException(nameof(locations));

            foreach (var city in cities)
                locations.Add(GetByCity(city) ?? throw new InvalidOperationException());

            return locations;
        }
    }
}