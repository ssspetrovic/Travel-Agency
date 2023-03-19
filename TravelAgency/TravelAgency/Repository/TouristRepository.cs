using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Data.Sqlite;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class TouristRepository : RepositoryBase, ITouristRepository
    {
        public void Add(TouristModel tourModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(TouristModel tourModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public TouristModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TouristModel GetByUsername(string? username)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var tourist = new TouristModel();

            const string selectStatement = "select * from Tourist where Username = $Username";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Username", username);

            using var selectReader = selectCommand.ExecuteReader();

            var tourRepository = new TourRepository();

            if (selectReader.Read())
                tourist = new TouristModel(selectReader.GetInt32(0), selectReader.GetString(1), selectReader.GetString(2),
                    selectReader.GetString(3),
                    selectReader.GetString(4), selectReader.GetString(5), Role.Tourist,
                    tourRepository.GetById(selectReader.GetInt32(7)), (TouristCheck)selectReader.GetInt32(8), selectReader.GetInt32(9));

            return tourist;
        }

        public List<TouristModel> GetByTour(TourModel tour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var tourists = new List<TouristModel>();

            const string selectStatement = "select * from Tourist where Tour_Id = $Tour_Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Tour_Id", tour.Id);

            using var selectReader = selectCommand.ExecuteReader();
            while (selectReader.Read())
                tourists.Add(new TouristModel(selectReader.GetString(1), selectReader.GetString(2), selectReader.GetString(3), selectReader.GetString(4), selectReader.GetString(5), (Role) selectReader.GetInt32(6), tour, TouristCheck.Unchecked, selectReader.GetInt32(9)));
            
            return tourists;
        }

        public void CheckTourist(string username)
        {
            var tourist = GetByUsername(username);

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "update Tourist set IsChecked = $IsChecked where Username = $Username";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Username", username);

            if (tourist.TouristCheck == TouristCheck.Unchecked)
                selectCommand.Parameters.AddWithValue("$IsChecked", TouristCheck.Pinged);
            else
            {
                MessageBox.Show("Tourist has already been pinged before!");
                selectCommand.Parameters.AddWithValue("$IsChecked", tourist.TouristCheck);
            }

            selectCommand.ExecuteNonQuery();
        }

        public void RemoveTour(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string updateStatement = "update Tourist set Tour_Id = null where Id = $Id";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("$Id", id);
            updateCommand.ExecuteNonQuery();
        }

        public void CheckAllTourists(string tourists)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string updateStatement = "update Tourist set IsChecked = 1 where Username in ($Usernames)";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("$Usernames", tourists);
            updateCommand.ExecuteNonQuery();
        }
    }
}
