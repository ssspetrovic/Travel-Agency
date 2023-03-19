using Microsoft.Data.Sqlite;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class CurrentUserRepository : RepositoryBase, ICurrentUserRepository
    {
        public void Add(string username)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select Name from User where Username = $Username";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            selectCommand.Parameters.AddWithValue("$Username", username);

            using var selectReader = selectCommand.ExecuteReader();
            var name = "";
            if (selectReader.Read())
            {
                name = selectReader.GetString(0);
            }

            databaseConnection.Close();
            databaseConnection.Open();

            const string insertStatement = "insert into CurrentUser(Username, Name) values($Username, $Name)";
            using var insertCommand = new SqliteCommand(insertStatement, databaseConnection);
            insertCommand.Parameters.AddWithValue("$Username", username);
            insertCommand.Parameters.AddWithValue("$Name", name);
            insertCommand.ExecuteNonQuery();

        }

        public void Remove()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string deleteStatement = "delete from CurrentUser";
            using var deleteCommand = new SqliteCommand(deleteStatement, databaseConnection);
            deleteCommand.ExecuteNonQuery();

        }

        public CurrentUserModel Get()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select * from CurrentUser";
            using var selectCommand = new SqliteCommand( selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            return !selectReader.Read() ? new CurrentUserModel() : new CurrentUserModel(selectReader.GetString(0), selectReader.GetString(1));
        }
    }
}
