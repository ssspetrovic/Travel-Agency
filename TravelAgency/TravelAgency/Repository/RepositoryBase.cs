using Microsoft.Data.Sqlite;

namespace TravelAgency.Repository
{
    public abstract class RepositoryBase
    {
        private const string DatabasePath = "../../../Resources/Data/data.db";
        private const string ConnectionString = $"Data Source={DatabasePath}";

        protected static SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}
