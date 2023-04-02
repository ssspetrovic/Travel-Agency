using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Data.Sqlite;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class TouristRepository : RepositoryBase, ITouristRepository
    {
        public void Add(Tourist tour)
        {
            throw new NotImplementedException();
        }

        public void Edit(Tourist tour)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Tourist GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Tourist GetByUsername(string? username)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var tourist = new Tourist();

            const string selectStatement = "select * from Tourist where Username = $Username";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Username", username);

            using var selectReader = selectCommand.ExecuteReader();

            var tourRepository = new TourRepository();

            if (selectReader.Read())
                tourist = new Tourist(selectReader.GetInt32(0), selectReader.GetString(1), selectReader.GetString(2),
                    selectReader.GetString(3),
                    selectReader.GetString(4), selectReader.GetString(5), Role.Tourist,
                    tourRepository.GetById(selectReader.GetInt32(7)), (TouristAppearance) selectReader.GetInt32(8), selectReader.GetInt32(9), selectReader.GetInt32(10));

            return tourist;
        }

        public List<Tourist> GetByTour(Tour tour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var tourists = new List<Tourist>();

            const string selectStatement = "select * from Tourist where Tour_Id = $Tour_Id";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Tour_Id", tour.Id);

            using var selectReader = selectCommand.ExecuteReader();
            while (selectReader.Read())
                tourists.Add(new Tourist(selectReader.GetInt32(0), selectReader.GetString(1), selectReader.GetString(2), selectReader.GetString(3),
                    selectReader.GetString(4), selectReader.GetString(5), (Role) selectReader.GetInt32(6), 
                    tour, TouristAppearance.Unknown, selectReader.GetInt32(9), selectReader.GetInt32(10)));
            
            return tourists;
        }

        public void CheckTouristAppearance(string username)
        {
            var tourist = GetByUsername(username);

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "update Tourist set IsChecked = $IsChecked where Username = $Username";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Username", username);

            //Ako turistu nismo cekirali, to znaci da mu mi moramo slati ping da se prijavi da je prisutan
            if (tourist.TouristAppearance == TouristAppearance.Unknown)
                selectCommand.Parameters.AddWithValue("$IsChecked", TouristAppearance.Pinged);
            //U suprotnom je turista vec pozvan
            else
            {
                MessageBox.Show("Tourist " + username + " has already been pinged before!");
                selectCommand.Parameters.AddWithValue("$IsChecked", tourist.TouristAppearance);
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

        public void CheckAllTouristAppearances(string tourists)
        {
            var touristList = tourists.Split(", ").ToList();
            foreach(var tourist in touristList)
                CheckTouristAppearance(tourist);
        }

        //public ChartValues<int> GetAgeGroups()
        //{

        //}
    }
}
