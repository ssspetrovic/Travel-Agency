using System.Collections.ObjectModel;
using TravelAgency.DTO;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class MyTourDtoRepository : RepositoryBase, IMyTourDtoRepository
    {
        public void Add(MyTourDto myTour)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var insertCommand = databaseConnection.CreateCommand();
            insertCommand.CommandText =
                @"
                    INSERT INTO MyTourDto (TourId, TouristUsername, Name, City, Country, Date, Status, KeyPoint)
                    VALUES ($TourId, $TouristUsername, $Name, $City, $Country, $Date, $Status, $KeyPoint)
                ";
            insertCommand.Parameters.AddWithValue("TourId", myTour.TourId);
            insertCommand.Parameters.AddWithValue("TouristUsername", myTour.TouristUsername);
            insertCommand.Parameters.AddWithValue("$Name", myTour.Name);
            insertCommand.Parameters.AddWithValue("$City", myTour.Location.City);
            insertCommand.Parameters.AddWithValue("$Country", myTour.Location.Country);
            insertCommand.Parameters.AddWithValue("$Date", myTour.Date);
            insertCommand.Parameters.AddWithValue("$Status", (int)myTour.Status);
            insertCommand.Parameters.AddWithValue("$KeyPoint", myTour.KeyPoint);
            insertCommand.ExecuteNonQuery();
        }

        public void UpdateStatus(string tourName, MyTourDto.TourStatus newStatus)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE MyTourDto
                    SET Status = $newStatus
                    WHERE Name = $tourName
                ";
            
            updateCommand.Parameters.AddWithValue("$newStatus", (int)newStatus);
            updateCommand.Parameters.AddWithValue("$tourName", tourName);
            updateCommand.ExecuteNonQuery();
        }

        public void UpdateKeyPoint(string tourName, string newKeyPoint)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var updateCommand = databaseConnection.CreateCommand();
            updateCommand.CommandText =
                @"
                    UPDATE MyTourDto
                    SET KeyPoint = $newKeyPoint
                    WHERE Name = $tourName
                ";

            updateCommand.Parameters.AddWithValue("$newKeyPoint", newKeyPoint);
            updateCommand.Parameters.AddWithValue("$tourName", tourName);
            updateCommand.ExecuteNonQuery();
        }

        public ObservableCollection<MyTourDto> GetAllAsCollection()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM MyTourDto WHERE TouristUsername = $CurrentUserUsername";
            selectCommand.Parameters.AddWithValue("$CurrentUserUsername", CurrentUser.Username);
            using var selectReader = selectCommand.ExecuteReader();

            var vouchers = new ObservableCollection<MyTourDto>();
            while (selectReader.Read())
            {
                vouchers.Add(new MyTourDto(
                    selectReader.GetInt32(0),
                    selectReader.GetString(1),
                    selectReader.GetString(2),
                    new Location(selectReader.GetString(3), selectReader.GetString(4)),
                    selectReader.GetDateTime(5),
                    (MyTourDto.TourStatus)selectReader.GetInt32(6),
                    selectReader.GetString(7)
                ));
            }

            return vouchers;
        }
    }
}
