using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using LiveCharts;
using TravelAgency.Model;
using TravelAgency.Repository;
using LiveCharts.Defaults;

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

                if (!DateTime.TryParse(dateRange.Split(" - ")[0], out var startDate) ||
                    !DateTime.TryParse(dateRange.Split(" - ")[1], out var endDate) ||
                    !DateTime.TryParse(parameter, out var date)) continue;

                if (date >= startDate && date <= endDate)
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
            if (dataType == "Location")
                selectStatement += @"Location_Id = (select Location_Id from (select Location_Id, count(Location_Id) as 
                        Location_Count from RequestedTour group by Location_Id order by Location_Count desc limit 1) as Max_Location)";
            else
                selectStatement += @"Language = (select Language from (select Language, count(Language) as 
                        Language_Count from RequestedTour group by Language order by Language_Count desc limit 1) as Max_Language)";
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

        public ChartValues<double> GetComparisons(ObservableCollection<RequestTour> requestedTours, string dataType)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var countStatement = new List<int>();
            var chartData = new ChartValues<double> { requestedTours.Count };

            var selectStatement = "select count(*) from RequestedTour group by ";

            if (dataType == "Location")
                selectStatement += "Location_Id";
            else
                selectStatement += "Language";

            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            while (selectReader.Read())
                countStatement.Add(selectReader.GetInt32(0));
            countStatement.Remove(requestedTours.Count);

            foreach(var count in countStatement)
                chartData.Add(count);

            return chartData;
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
    }
}
