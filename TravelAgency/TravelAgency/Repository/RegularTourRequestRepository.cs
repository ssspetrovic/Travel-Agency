using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class RegularTourRequestRepository : RepositoryBase
    {
        public void Add(RegularTourRequest tourRequest)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO RegularTourRequest (TouristUsername, City, Country, Language, DateRange, Description)
                    VALUES ($TouristUsername, $City, $Country, $Language, $DateRange, $Description)
                ";
            insertCommand.Parameters.AddWithValue("$TouristUsername", tourRequest.TouristUsername);
            insertCommand.Parameters.AddWithValue("$City", tourRequest.Location.City);
            insertCommand.Parameters.AddWithValue("$Country", tourRequest.Location.Country);
            insertCommand.Parameters.AddWithValue("$Language", (int)tourRequest.Language);
            insertCommand.Parameters.AddWithValue("$DateRange", $"{tourRequest.StartingDate.Date} - {tourRequest.EndingDate.Date}");
            insertCommand.Parameters.AddWithValue("$Description", tourRequest.Description);
            insertCommand.ExecuteNonQuery();
        }
        

        public RegularTourRequestRepository() { }
    }
}
