﻿using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class TouristService : RepositoryBase
    {
        private readonly TouristRepository _touristRepository;
        private readonly TourService _tourService;

        public TouristService()
        {
            _touristRepository = new TouristRepository();
            _tourService = new TourService();
        }

        public List<Tourist> GetByTour(Tour tour)
        {
            return _touristRepository.GetByTour(tour);
        }

        public void RemoveTour(int id)
        {
            _touristRepository.RemoveTour(id);
        }

        public void CheckTouristAppearance(string username)
        {
            var tourist = GetByUsername(username);

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "update Tourist set IsChecked = $IsChecked where Username = $Username";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Username", username);

            if (tourist.TouristAppearance == TouristAppearance.Unknown)
                selectCommand.Parameters.AddWithValue("$IsChecked", TouristAppearance.Pinged);
            else
            {
                MessageBox.Show("Tourist " + username + " has already been pinged before!");
                selectCommand.Parameters.AddWithValue("$IsChecked", tourist.TouristAppearance);
            }

            selectCommand.ExecuteNonQuery();
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
            var tour = new Tour();

            if (selectReader.Read())
            {
                if (!selectReader.IsDBNull(7))
                    tour = _tourService.GetById(selectReader.GetInt32(7));
                tourist = new Tourist(selectReader.GetInt32(0), selectReader.GetString(1), selectReader.GetString(2),
                    selectReader.GetString(3),
                    selectReader.GetString(4), selectReader.GetString(5), Role.Tourist,
                    tour, (TouristAppearance)selectReader.GetInt32(8), selectReader.GetInt32(9), selectReader.GetInt32(10));
            }

            return tourist;
        }

        public void CheckAllTouristAppearances(string tourists)
        {
            var touristList = tourists.Split(", ").ToList();
            foreach (var tourist in touristList)
                CheckTouristAppearance(tourist);
        }
    }
}