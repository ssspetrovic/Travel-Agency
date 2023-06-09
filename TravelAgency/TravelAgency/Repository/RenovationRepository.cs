using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace TravelAgency.Repository
{
    public class RenovationRepository : RepositoryBase, IRenovationRepository
    {
        public void Add(Renovation renovation)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into Renovation (AccommodationId, StartDate, EndDate, Duration, Description) 
                    values ($AccommodationId, $StartDate, $EndDate, $Duration, $Description)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$AccommodationId", renovation.AccommodationId);
            insertCommand.Parameters.AddWithValue("$StartDate", renovation.StartDate);
            insertCommand.Parameters.AddWithValue("$EndDate", renovation.EndDate);
            insertCommand.Parameters.AddWithValue("$Duration", renovation.Duration);
            insertCommand.Parameters.AddWithValue("$Description", renovation.Description);
            insertCommand.ExecuteNonQuery();
        }

        public ObservableCollection<Renovation> GetAll()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select Id, AccommodationId, date(StartDate), date(EndDate), Duration, Description from Renovation";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var renovationList = new ObservableCollection<Renovation>();

            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var accId = selectReader.GetInt32(1);
                var startDate = selectReader.GetDateTime(2);
                var endDate = selectReader.GetDateTime(3);
                var duration = selectReader.GetInt32(4);
                var description = selectReader.GetString(5);

                Renovation r = new Renovation(id,accId,startDate,endDate,duration,description);
                
                renovationList.Add(r);
            }

            return renovationList;
        }

        public ObservableCollection<Renovation> GetAllByAccommodationId(int accommodationId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select Id, AccommodationId, date(StartDate), date(EndDate), Duration, Description from Renovation where AccommodationId = $accId";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$accId", accommodationId);
            using var selectReader = selectCommand.ExecuteReader();

            var renovationList = new ObservableCollection<Renovation>();

            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var accId = selectReader.GetInt32(1);
                var startDate = selectReader.GetDateTime(2);
                var endDate = selectReader.GetDateTime(3);
                var duration = selectReader.GetInt32(4);
                var description = selectReader.GetString(5);

                Renovation r = new Renovation(id, accId, startDate, endDate, duration, description);

                renovationList.Add(r);
            }

            return renovationList;
        }

        public void Remove(int renovationId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    DELETE from Renovation where Id = $Id;
                ";
            updateCommand.Parameters.AddWithValue("$Id", renovationId);
            updateCommand.ExecuteNonQuery();
        }
    }
}
