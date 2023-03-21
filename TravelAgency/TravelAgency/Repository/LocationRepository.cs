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
        public void Add(Location location)
        {
            throw new NotImplementedException();
        }

        public void Edit(Location location)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

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


            //Posto logika kljucnih tacki u nasoj datoteci se svodi na tome da je KeyPoints text parametar u tabelama postoje dve situacije koje se mogu desiti
            //Prva: Zapravo saljemo grad iz tabele Location i onda samo vrsimo izvlacenje iz tabele
            //Druga: Saljemo string id lokacije koji moramo da konvertujemo u int, pri cemu cemo zapravo naci lokaciju po id-u
            if (int.TryParse(city, out var cityId))
            {
                const string selectStatement = @"select * from Location where Id = $Id ";
                using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
                selectCommand.Parameters.AddWithValue("$Id", cityId);
                using var selectReader = selectCommand.ExecuteReader();

                if (selectReader.Read())
                    return new Location(selectReader.GetInt32(0), selectReader.GetString(1),
                        selectReader.GetString(2));
            }

            else
            {
                const string selectStatement = @"select * from Location where City = $City ";
                using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
                selectCommand.Parameters.AddWithValue("$City", city);
                using var selectReader = selectCommand.ExecuteReader();

                if (selectReader.Read())
                    return new Location(selectReader.GetInt32(0), selectReader.GetString(1),
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

        public List<Location?> GetByAllCities(List<string> cities)
        {
            return cities.Select(GetByCity).ToList();
        }
    }
}
