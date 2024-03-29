﻿using System;
using System.Net;
using Microsoft.Data.Sqlite;
using TravelAgency.Interface;
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

        public User GetById(int id)
        {
            User user = null!;
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = @"SELECT * FROM User WHERE Id = $Id";

            selectCommand.Parameters.AddWithValue("$Id", id);
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

        public void RemoveByUsername(string? username)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            const string deleteStatement = "delete from User where Username = $Username";
            using var deleteCommand = new SqliteCommand(deleteStatement, databaseConnection);
            deleteCommand.Parameters.AddWithValue("$Username", username);
            deleteCommand.ExecuteNonQuery();
        }

        public void SetSuperGuide(string? username, bool isSuperGuide)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            const string updateStatement = "update Guide set IsSuperGuide = $IsSuperGuide where Username = $Username";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);

            updateCommand.Parameters.AddWithValue("$IsSuperGuide", isSuperGuide ? 1 : 0);
            updateCommand.Parameters.AddWithValue("$Username", username);
            updateCommand.ExecuteNonQuery();
        }
    }
}
