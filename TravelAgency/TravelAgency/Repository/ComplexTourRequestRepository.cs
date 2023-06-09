using System.Collections.ObjectModel;
using TravelAgency.Interface;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class ComplexTourRequestRepository : RepositoryBase, IComplexTourRequestRepository
    {
        public void Add(ComplexRequestTour complexTourRequest)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO ComplexTour (RequestedTourIds, DateRange, Location_Id, Description, Language, NumberOfGuests, Status, AcceptedDate, TouristUsername)
                    VALUES ($RequestedTourIds, $DateRange, $Location_Id, $Description, $Language, $NumberOfGuests, $Status, $AcceptedDate, $TouristUsername)
                ";

            insertCommand.Parameters.AddWithValue("$RequestedTourIds", complexTourRequest.RequestTourIds);
            insertCommand.Parameters.AddWithValue("$DateRange", complexTourRequest.DateRange);
            insertCommand.Parameters.AddWithValue("$Location_Id", complexTourRequest.Location.Id);
            insertCommand.Parameters.AddWithValue("$Description", complexTourRequest.Description);
            insertCommand.Parameters.AddWithValue("$Language", complexTourRequest.Language);
            insertCommand.Parameters.AddWithValue("$NumberOfGuests", complexTourRequest.MaxGuests);
            insertCommand.Parameters.AddWithValue("$Status", complexTourRequest.Status);
            insertCommand.Parameters.AddWithValue("$AcceptedDate", complexTourRequest.AcceptedDate);
            insertCommand.Parameters.AddWithValue("$TouristUsername", complexTourRequest.TouristUsername);

            insertCommand.ExecuteNonQuery();
        }


        public void DeleteById(int id)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var deleteCommand = databaseConnection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM ComplexTour WHERE Id = $id";
            deleteCommand.Parameters.AddWithValue("$id", id);
            deleteCommand.ExecuteNonQuery();
        }

        public ObservableCollection<ComplexRequestTour> GetAllAsCollection()
        { 
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText =
                @"
                    SELECT Id, RequestedTourIds, DateRange, Location_Id, Description, Language, NumberOfGuests, Status, AcceptedDate, TouristUsername
                    FROM ComplexTour where touristUsername = $touristUsername
                ";
            selectCommand.Parameters.AddWithValue("$touristUsername", CurrentUser.Username);
            selectCommand.ExecuteNonQuery();

            using var selectReader = selectCommand.ExecuteReader();
            var complexTourRequests = new ObservableCollection<ComplexRequestTour>();

            while (selectReader.Read())
            {
                var id = selectReader.GetInt32(0);
                var requestedTourIds = selectReader.GetString(1);
                var dateRange = selectReader.GetString(2);
                var locationId = selectReader.GetInt32(3);
                var description = selectReader.GetString(4);
                var language = (Language)selectReader.GetInt32(5);
                var numberOfGuests = selectReader.GetInt32(6);
                var status = (Status)selectReader.GetInt32(7);
                var acceptedDate = selectReader.GetString(8);
                var touristUsername = selectReader.GetString(9);

                var location = new LocationRepository().GetById(locationId);
                var complexTourRequest = new ComplexRequestTour(id, requestedTourIds, dateRange, location!, description,
                    language, numberOfGuests, status, acceptedDate, touristUsername);

                complexTourRequests.Add(complexTourRequest);
            }

            return complexTourRequests;
        }

        public int GetLastId()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT MAX(Id) FROM ComplexTour";

            using var selectReader = selectCommand.ExecuteReader();
            return !selectReader.Read() ? 1 : selectReader.GetInt32(0) + 1;
        }

        public void UpdateStatus(int id, Status newStatus)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE ComplexTour
                    SET Status = $newStatus
                    WHERE Id = $id
                ";
            updateCommand.Parameters.AddWithValue("$newStatus", newStatus);
            updateCommand.Parameters.AddWithValue("$id", id);
            updateCommand.ExecuteNonQuery();
        }
    }
}
