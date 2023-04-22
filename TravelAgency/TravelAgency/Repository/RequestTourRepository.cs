using Microsoft.Data.Sqlite;
using System.Data;

namespace TravelAgency.Repository
{
    internal class RequestTourRepository : RepositoryBase
    {
        public DataTable GetAllAsDataTable(DataTable dt)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from RequestedTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);

            dt.Load(selectCommand.ExecuteReader());
            return dt;
        }
    }
}
