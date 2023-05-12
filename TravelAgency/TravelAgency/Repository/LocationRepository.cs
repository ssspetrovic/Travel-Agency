using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelAgency.DTO;
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

            const string selectStatement = @"select * from Location where City = $City";
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

        public ObservableCollection<Location> GetAll()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var locations = new ObservableCollection<Location>();

            const string selectStatement = "select * from Location";
            using var selectCommand = new SqliteCommand( selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            while(selectReader.Read())
                locations.Add(new Location(selectReader.GetInt32(0), selectReader.GetString(1), selectReader.GetString(2)));

            return locations;
        }

        public string FindLocationIdByText(string text)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "SELECT * FROM Location WHERE City LIKE '%' || $City || '%' OR Country LIKE '%' || $Country || '%'";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$City", text);
            selectCommand.Parameters.AddWithValue("$Country", text);


            using var selectReader = selectCommand.ExecuteReader();
            var ids = "";

            while (selectReader.Read())
                ids += ", " + selectReader.GetInt32(0);
            return ids.Length == 0 ? "Empty" : ids.Substring(2, ids.Length - 2);
        }
    }
}
