using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class TourRepository : RepositoryBase, ITourRepository
    {
        private const string DatabaseFilePath = "../../../Resources/Data/data.db";

        public void Add(TourModel tourModel)
        {
            using var databaseConnection = new SqliteConnection("Data Source=" + DatabaseFilePath);
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Tour(Name, Location_Id, Description, Language, MaxGuests, Birthdate, Duration, Images) 
                    values ($Name, $Location_Id, $Description, $Language, $MaxGuests, $Birthdate, $Duration, $Images)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$Name", tourModel.Name);
            insertCommand.Parameters.AddWithValue("$Location_Id", tourModel.Location.Id);
            insertCommand.Parameters.AddWithValue("$Description", tourModel.Description);
            insertCommand.Parameters.AddWithValue("$Language", tourModel.Language);
            insertCommand.Parameters.AddWithValue("$MaxGuests", tourModel.MaxGuests);
            insertCommand.Parameters.AddWithValue("$Birthdate", tourModel.Birthdate);
            insertCommand.Parameters.AddWithValue("$Duration", tourModel.Duration);
            insertCommand.Parameters.AddWithValue("$Images", tourModel.Images);
            insertCommand.ExecuteNonQuery();

        }

        public void Edit(TourModel tourModel)
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

        public UserModel GetByName(string? name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }
    }
}
