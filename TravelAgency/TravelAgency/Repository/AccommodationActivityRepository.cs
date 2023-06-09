using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.Repository
{
    public class AccommodationActivityRepository : RepositoryBase, IAccommodationActivityRepository
    {
        public void Add(AccommodationActivity activity)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string insertStatement =
                @"insert into AccommodationActivity(AccommodationId, StartDate, CanceledReservation, DelayedReservation, RenovationSuggestion) 
                    values ($AccommodationId, $StartDate, $CanceledReservation, $DelayedReservation, $RenovationSuggestion)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$AccommodationId", activity.AccommodationId);
            insertCommand.Parameters.AddWithValue("$StartDate", activity.StartDate);
            insertCommand.Parameters.AddWithValue("$CanceledReservation", activity.CanceledReservation);
            insertCommand.Parameters.AddWithValue("$DelayedReservation", activity.DelayedReseravation);
            insertCommand.Parameters.AddWithValue("$RenovationSuggestion", activity.RenovationSuggestion);
            insertCommand.ExecuteNonQuery();
        }

        public List<AccommodationActivity> GetAllByAccommodationId(int accommodationId)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = @"select * from AccommodationActivity where AccommodationId = $AccommodationId";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$AccommodationId", accommodationId);
            using var selectReader = selectCommand.ExecuteReader();

            var activityList = new List<AccommodationActivity>();


            while (selectReader.Read())
            {
                int accId = selectReader.GetInt32(0);
                DateTime startDate = selectReader.GetDateTime(1);
                int canceledReservation = selectReader.GetInt32(2);
                int delayedReservation = selectReader.GetInt32(3);
                int renovationSuggestion = selectReader.GetInt32(4);

                AccommodationActivity activity = new AccommodationActivity(accId, startDate,canceledReservation,delayedReservation,renovationSuggestion);
                activityList.Add(activity);
                }

            return activityList;
        }
    }
}
