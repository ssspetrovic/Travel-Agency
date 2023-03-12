using System;
using System.Collections.Generic;
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
            bool validUser;
            const string databaseFilePath = "./database.sqlite3";

            using (var databaseConnection = new SqliteConnection("Data Source=" + databaseFilePath))
            {
                databaseConnection.Open();
                var selectCommand = databaseConnection.CreateCommand();
                selectCommand.CommandText = @"SELECT * FROM User WHERE Username = $Username AND Password = $Password";

                selectCommand.Parameters.AddWithValue("$Username", credential.UserName);
                selectCommand.Parameters.AddWithValue("$Password", credential.Password);
                using var selectReader = selectCommand.ExecuteReader();

                var selectedUsers = new List<UserModel>();

                while (selectReader.Read())
                {
                    selectedUsers.Add(new UserModel(selectReader.GetInt32(0), selectReader.GetString(1), selectReader.GetString(2)));
                }

                return validUser = selectedUsers.Capacity == 0 ? false : true;
            }

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
