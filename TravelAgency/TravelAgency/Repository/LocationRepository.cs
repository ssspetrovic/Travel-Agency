using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class LocationRepository : RepositoryBase, ILocationRepository
    {
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

        public LocationModel? GetById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from Location where Id = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", id);
            using var selectReader = selectCommand.ExecuteReader();

            if(selectReader.Read())
                return new LocationModel(selectReader.GetInt32(0), selectReader.GetString(1),
                    selectReader.GetString(2));
            return null;
        }

        public LocationModel? GetByCity(string city)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            if(int.TryParse(city, out var cityId))
            {
                const string selectStatement = @"select * from Location where Id = $Id ";
                using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
                selectCommand.Parameters.AddWithValue("$Id", cityId);
                using var selectReader = selectCommand.ExecuteReader();

                if (selectReader.Read())
                    return new LocationModel(selectReader.GetInt32(0), selectReader.GetString(1),
                        selectReader.GetString(2));
            }

            else
            {
                const string selectStatement = @"select * from Location where City = $City ";
                using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
                selectCommand.Parameters.AddWithValue("$City", city);
                using var selectReader = selectCommand.ExecuteReader();

                if (selectReader.Read())
                    return new LocationModel(selectReader.GetInt32(0), selectReader.GetString(1),
                        selectReader.GetString(2));

            }

            return null;
        }

        public DataTable GetByAll(DataTable dt)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from Location";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            dt.Load(selectCommand.ExecuteReader());
            return dt;
        }

        public List<LocationModel?> GetByAllCities(List<string> cities)
        {
            return cities.Select(GetByCity).ToList();
        }
    }
}
