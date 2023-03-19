using System;
using System.Collections.Generic;
using System.Net;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        //private const string DatabaseFilePath = "../../../Resources/Data/data.db";
        public bool AuthenticateUser(NetworkCredential credential)
        {
            using var databaseConnection = GetConnection();
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
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = @"SELECT Role FROM User WHERE Username = $Username";

            selectCommand.Parameters.AddWithValue("$Username", username);
            using var selectReader = selectCommand.ExecuteReader();

            selectReader.Read();
            var role = (Role)Convert.ToInt32(selectReader["Role"]);
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

        public UserModel GetByUsername(string? username)
        {
            UserModel user = null!;
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = @"SELECT * FROM User WHERE Username = $Username";

            selectCommand.Parameters.AddWithValue("$Username", username);
            using var selectReader = selectCommand.ExecuteReader();

            if (selectReader.Read())
            {
                user = new UserModel()
                {
                    Id = selectReader.GetInt32(0),
                    UserName = selectReader.GetString(1),
                    Password = string.Empty,
                    Name = selectReader.GetString(3),
                    Surname = selectReader.GetString(4),
                    Email = selectReader.GetString(5),
                    Role = (Role)selectReader.GetInt32(6)
                };
            }

            return user;
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }
    }
}
