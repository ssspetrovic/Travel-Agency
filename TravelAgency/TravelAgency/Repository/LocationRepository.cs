using Microsoft.Data.Sqlite;
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

        public UserModel GetById(int id)
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

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }
    }
}
