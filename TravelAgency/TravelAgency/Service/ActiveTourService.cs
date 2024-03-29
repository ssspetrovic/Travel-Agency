﻿using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Windows.Forms;
using TravelAgency.Interface;
using TravelAgency.Model;
using TravelAgency.Repository;
using static System.Int32;

namespace TravelAgency.Service
{
    public class ActiveTourService : RepositoryBase
    {
        private readonly IActiveTourRepository _activeTourRepository;
        private readonly LocationService _locationService;

        public string CurrentKeyPoint { get; set; } = null!;

        public ActiveTourService()
        {
            _activeTourRepository = new ActiveTourRepository();
            _locationService = new LocationService();
        }

        private string FindUpdate(string currentKeyPoint)
        {
            currentKeyPoint = _locationService.GetByCity(currentKeyPoint)!.Id.ToString();

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select KeyPointsList from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var keyPoints = "";

            if (selectReader.Read())
            {
                keyPoints = FindCurrentKeyPoint(currentKeyPoint, selectReader.GetString(0));
            }

            databaseConnection.Close();
            return keyPoints;
        }


        public void UpdateKeyPoint(string currentKeyPoint)
        {
            var keyPoints = FindUpdate(currentKeyPoint);
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            CurrentKeyPoint = currentKeyPoint;
            const string updateStatement = "update ActiveTour set KeyPointsList = $KeyPointsList, CurrentKeyPoint = $CurrentKeyPoint";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("KeyPointsList", keyPoints);
            updateCommand.Parameters.AddWithValue("$CurrentKeyPoint", CurrentKeyPoint);
            updateCommand.ExecuteNonQuery();

        }

        public string KeyPointsValidation(string currentKeyPoint, string lastKeyPoint, int index, List<string> allKeyPoints)
        {
            var location = allKeyPoints[index];

            if (location.Contains(currentKeyPoint + ":True"))
            {
                MessageBox.Show("We already passed " + _locationService.GetById(Parse(currentKeyPoint))!.City + "!");
                return lastKeyPoint;
            }

            if (!location.Contains(currentKeyPoint + ":False")) return allKeyPoints[index];

            if (lastKeyPoint.Contains(":True"))
            {
                allKeyPoints[index] = currentKeyPoint + ":True";
                CurrentKeyPoint = currentKeyPoint;
                return lastKeyPoint;
            }
            
            MessageBox.Show("You can't skip key points!");
            return lastKeyPoint;
        }

        public string FindCurrentKeyPoint(string currentKeyPoint, string keyPoints)
        {
            var allKeyPoints = keyPoints.Split(", ").ToList();
            var lastKeyPoint = "";

            for (var i = 0; i < allKeyPoints.Capacity; i++)
                lastKeyPoint = KeyPointsValidation(currentKeyPoint, lastKeyPoint, i, allKeyPoints);

            return string.Join(", ", allKeyPoints);
        }

        public string GetActiveTourColumn(string column)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement = "select " + column + " from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            return selectReader.Read() ? selectReader.GetString(0) : "Error";
        }

        public void RemoveLastKeyPoint()
        {
            var lastKeyPoint = GetActiveTourColumn("KeyPointsList").Split(", ").Last().Split(":")[0];
            if(lastKeyPoint != "Error")
                CurrentKeyPoint = _locationService.GetById(Parse(lastKeyPoint))!.City;
            _activeTourRepository.Remove();
        }

        public bool IsActive()
        {
            return _activeTourRepository.IsActive();
        }

        public void Add(ActiveTour activeTour)
        {
            _activeTourRepository.Add(activeTour);
        }

        public bool ExistsByName(string name)
        {
            return _activeTourRepository.ExistsByName(name);
        }

        public string GetCurrentKeyPointByName(string name)
        {
            return _activeTourRepository.GetCurrentKeyPointByName(name);
        }
    }
}
