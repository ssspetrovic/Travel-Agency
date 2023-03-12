using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public bool AuthenticateUser(NetworkCredential credential)
        {
            const string databaseFilePath = "./database.sqlite3";

            using var databaseConnection = new SqliteConnection("Data Source=" + databaseFilePath);
            databaseConnection.Open();

            var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = @"SELECT * FROM User WHERE Username = $Username AND Password = $Password";

            selectCommand.Parameters.AddWithValue("$Username", credential.UserName);
            selectCommand.Parameters.AddWithValue("$Password", credential.Password);
            using var selectReader = selectCommand.ExecuteReader();

            return selectReader.Read() && selectReader["Username"] != DBNull.Value;
        }

        public Role GetRole(string username)
        {
            const string databaseFilePath = "./database.sqlite3";

            using var databaseConnection = new SqliteConnection("Data Source=" + databaseFilePath);
            databaseConnection.Open();
            var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = @"SELECT Role FROM User WHERE Username = $Username";

            selectCommand.Parameters.AddWithValue("$Username", username);
            using var selectReader = selectCommand.ExecuteReader();

            selectReader.Read(); 
            var role =(Role) Convert.ToInt32(selectReader["Role"]);
            return role;
        }

        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(UserModel userModel)
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

        public UserModel GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }


    }
}
