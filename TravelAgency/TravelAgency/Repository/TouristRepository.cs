using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class TouristRepository : RepositoryBase
    {
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

        public void RemoveTour(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string updateStatement = "update Tourist set Tour_Id = null where Id = $Id";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("$Id", id);
            updateCommand.ExecuteNonQuery();
        }

        

    }
}
