using System;
using System.Collections.Generic;
using System.Net;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        //Autentikacija prijavljivanja korisnika
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

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Edit(User user)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string? username)
        {
            User user = null!;
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = @"SELECT * FROM User WHERE Username = $Username";

            selectCommand.Parameters.AddWithValue("$Username", username);
            using var selectReader = selectCommand.ExecuteReader();

            if (selectReader.Read())
            {
                user = new User()
                {
                    Id = selectReader.GetInt32(0),
                    UserName = selectReader.GetString(1),
                    Password = selectReader.GetString(2),
                    Name = selectReader.GetString(3),
                    Surname = selectReader.GetString(4),
                    Email = selectReader.GetString(5),
                    Role = (Role)selectReader.GetInt32(6)
                };
            }

            return user;
        }

        public IEnumerable<User> GetByAll()
        {
            throw new NotImplementedException();
        }
    }
}
