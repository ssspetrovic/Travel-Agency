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
                    INSERT INTO TourReservation ()
                    VALUES ()
                ";
        }
        

        public RegularTourRequestRepository() { }
    }
}
