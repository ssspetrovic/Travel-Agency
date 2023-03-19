using Microsoft.Data.Sqlite;

namespace TravelAgency.Repository
{
    public abstract class RepositoryBase
    {
        private const string ConnectionString = "Data Source=../../../Resources/Data/data.db";

        //public const string DataBaseConnection = "Resources/Data/data.db";

        protected static SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}
