﻿using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using LiveCharts;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class RequestTourService : RepositoryBase
    {
        private readonly RequestTourRepository _requestTourRepository;
        private readonly LocationService _locationService;

        public RequestTourService()
        {
            _requestTourRepository = new RequestTourRepository();
            _locationService = new LocationService();
        }

        public DataTable GetAllAsDataTable(DataTable dt)
        {
            return _requestTourRepository.GetAllAsDataTable(dt);
        }

        public string GetMostRequestedStat(string column)
        {
            return _requestTourRepository.GetMostRequestedStat(column);
        }

        private List<RequestTour> FindByDateRange(string parameter)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from RequestedTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            using var selectReader = selectCommand.ExecuteReader();
            var requestedTours = new List<RequestTour>();

            while (selectReader.Read())
            {
                var dateRange = selectReader.GetString(5);


                var formattedStartDate = DateTime.ParseExact(dateRange.Split(" - ")[0], "MM/dd/yyyy", new CultureInfo("en-US"));
                var formattedEndDate = DateTime.ParseExact(dateRange.Split(" - ")[1], "MM/dd/yyyy", new CultureInfo("en-US"));
                var date = DateTime.Parse(parameter);

                if (date >= formattedStartDate && date <= formattedEndDate)
                    requestedTours.Add(new RequestTour(selectReader.GetInt32(0), _locationService.GetById(selectReader.GetInt32(1))!, selectReader.GetString(2),
                        (Language)Enum.Parse(typeof(Language), selectReader.GetString(3)), selectReader.GetInt32(4), selectReader.GetString(5),
                        (Status)selectReader.GetInt32(6), selectReader.IsDBNull(7) ? "Empty" : selectReader.GetString(7)));
            }

            return requestedTours;
        }

        public List<RequestTour> FindByParameter(string parameter, string parameterName)
        {
            if (parameterName == "DateRange")
                return FindByDateRange(parameter);

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement = "select * from RequestedTour where " + parameterName + " = ";
            if (parameterName == "Language")
                selectStatement += "'" + parameter + "'";
            else
                selectStatement += parameter;
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            using var selectReader = selectCommand.ExecuteReader();
            var requestedTours = new List<RequestTour>();

            while (selectReader.Read())
                requestedTours.Add(new RequestTour(selectReader.GetInt32(0), _locationService.GetById(selectReader.GetInt32(1))!, selectReader.GetString(2),
                    (Language)Enum.Parse(typeof(Language), selectReader.GetString(3)), selectReader.GetInt32(4), selectReader.GetString(5), 
                    (Status)selectReader.GetInt32(6), selectReader.IsDBNull(7) ? "Empty" : selectReader.GetString(7)));

            return requestedTours;
        }

        public DataTable UpdateDataTable(DataTable dt, string ids)
        {
            return ids == "Empty" ? dt : _requestTourRepository.UpdateDataTable(dt, ids);
        }

        public string GetSelectStatementForCollection(string dataType)
        {
            var selectStatement = "select * from RequestedTour where ";
            if (dataType.Split(":")[0] == "Location")
                selectStatement += @"Location_Id = (select Location_Id from (select Location_Id, count(Location_Id) as 
                        Location_Count from RequestedTour where Location_Id = " + _locationService.GetByCity(dataType.Split(":")[1].Split(", ")[0].Trim())!.Id +" group by Location_Id order by Location_Count desc limit 1) as Max_Location)";
            else
                selectStatement += @"Language = (select Language from (select Language, count(Language) as 
                        Language_Count from RequestedTour where Language = '" + dataType.Split(":")[1].Trim() + "' group by Language order by Language_Count desc limit 1) as Max_Language)";
            return selectStatement;
        }

        public ObservableCollection<RequestTour> GetAllTimeRequestedTour(string dataType)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement = GetSelectStatementForCollection(dataType);
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();
            var requestedTours = new ObservableCollection<RequestTour>();

            while (selectReader.Read())
                requestedTours.Add(new RequestTour(selectReader.GetInt32(0), _locationService.GetById(selectReader.GetInt32(1))!, selectReader.GetString(2),
                    (Language)Enum.Parse(typeof(Language), selectReader.GetString(3)), selectReader.GetInt32(4), selectReader.GetString(5),
                    (Status)selectReader.GetInt32(6), selectReader.IsDBNull(7) ? "Empty" : selectReader.GetString(7)));

            return requestedTours;
        }

        public ObservableCollection<RequestTour> GetMostRequestedToursByYear(string dataType, string year)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement = GetSelectStatementForCollection(dataType) + " and DateRange like \"%" + year +"%\"";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();
            var requestedTours = new ObservableCollection<RequestTour>();

            while (selectReader.Read())
                requestedTours.Add(new RequestTour(selectReader.GetInt32(0), _locationService.GetById(selectReader.GetInt32(1))!, selectReader.GetString(2),
                    (Language)Enum.Parse(typeof(Language), selectReader.GetString(3)), selectReader.GetInt32(4), selectReader.GetString(5),
                    (Status)selectReader.GetInt32(6), selectReader.IsDBNull(7) ? "Empty" : selectReader.GetString(7)));

            return requestedTours;
        }

        public List<string> GetComparisonReader(string dataType, string dataContent)
        {
            var comparisonList = new List<string>();
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement = "select count(*), ";

            if (dataType == "Location")
                selectStatement += "Location_Id from RequestedTour group by Location_Id order by case Location_Id when " + _locationService.GetByCity(dataContent.Split(",")[0].Trim())!.Id + " then 0 else 1 end";
            else
                selectStatement += "Language from RequestedTour group by Language order by case Language when '" + dataContent.Trim() + "' then 0 else 1 end";

            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
            {
                if (dataType == "Location")
                    comparisonList.Add(selectReader.GetInt32(0) + ":" + _locationService.GetById(selectReader.GetInt32(1))!.City);
                else
                    comparisonList.Add(selectReader.GetInt32(0) + ":" + selectReader.GetString(1));
            }
            return comparisonList;
        }

        public ChartValues<double> GetComparisons(ObservableCollection<RequestTour> requestedTours, string dataType, string dataContent)
        {
            var countStatement = new List<int>();
            var comparisonList = GetComparisonReader(dataType, dataContent);

            foreach (var comparison in comparisonList)
                countStatement.Add(int.Parse(comparison.Split(":")[0]));

            var chartData = new ChartValues<double>();
            foreach (var count in countStatement)
                chartData.Add(count);

            return chartData;
        }

        public string[] GetComparisonLabels(ObservableCollection<RequestTour> tabAllData, string dataType, string dataContent)
        {
            var countLabels = new List<string>();
            var comparisonList = GetComparisonReader(dataType, dataContent);

            foreach (var comparison in comparisonList)
                countLabels.Add(comparison.Split(":")[1]);

            return countLabels.ToArray();
        }

        public List<Location> GetAllRequestedLocations()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var locations = new List<Location>();
            const string selectStatement = "select distinct Location_Id from RequestedTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
            {
                locations.Add(_locationService.GetById(selectReader.GetInt32(0))!);
            }

            return locations;
        }

        public List<string> GetAllRequestedLanguages()
        {
            return _requestTourRepository.GetAllRequestedLanguages();
        }

        public List<RequestTour> GetRequestsByDate(string day, int month, string year, string dataType)
        {
            var requestedTours = new List<RequestTour>();
            using var databaseConnection = GetConnection();
            databaseConnection.Open();
            var columnName = dataType.Split(":")[0] == "Location" ? "Location_Id" : "Language";
            var columnData = columnName == "Language"
                ? dataType.Split(":")[1].Trim()
                : _locationService.GetByCity(dataType.Split(":")[1].Split(", ")[0].Trim())!.Id.ToString();

            var selectStatement = "select * from RequestedTour where DateRange like '%' || $Month || '/%' || $Day || '%/' || '%' || $Year and " + columnName + " = $DataType";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Month", month);
            selectCommand.Parameters.AddWithValue("$Day", day);
            selectCommand.Parameters.AddWithValue("$Year", year);
            selectCommand.Parameters.AddWithValue("DataType", columnData);

            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
                requestedTours.Add(new RequestTour(selectReader.GetInt32(0), _locationService.GetById(selectReader.GetInt32(1))!, selectReader.GetString(2),
                    (Language)Enum.Parse(typeof(Language), selectReader.GetString(3)), selectReader.GetInt32(4), selectReader.GetString(5),
                    (Status)selectReader.GetInt32(6), selectReader.IsDBNull(7) ? "Empty" : selectReader.GetString(7)));

            requestedTours = requestedTours.Where(tour => {
                var dateRange = tour.DateRange.Split(" - ");
                var startDate = DateTime.Parse(dateRange[0]);
                var endDate = DateTime.Parse(dateRange[1]);
                return startDate.Month == month || endDate.Month == month;
            }).ToList();



            return requestedTours;
        }

    }
}