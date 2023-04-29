using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
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
    }
}
