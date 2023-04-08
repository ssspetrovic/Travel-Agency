using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class LocationRepository : RepositoryBase
    {
        public Location? GetById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from Location where Id = $Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Id", id);
            using var selectReader = selectCommand.ExecuteReader();

            if(selectReader.Read())
                return new Location(selectReader.GetInt32(0), selectReader.GetString(1),
                    selectReader.GetString(2));
            return null;
        }

        public Location? GetByCity(string city)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from Location where City = $City ";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$City", city);
            using var selectReader = selectCommand.ExecuteReader();

            if (selectReader.Read())
                return new Location(selectReader.GetInt32(0), selectReader.GetString(1),
                    selectReader.GetString(2));
            return null;
        }

        public List<Location?> GetByAllCities(List<string> cities)
        {
            return cities.Select(GetByCity).ToList();
        }
    }
}
