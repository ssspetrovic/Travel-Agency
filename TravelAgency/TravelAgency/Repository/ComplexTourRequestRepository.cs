using System.Collections.ObjectModel;
using TravelAgency.Interface;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    class ComplexTourRequestRepository : RepositoryBase, IComplexTourRequestRepository
    {
        public void Add(TourVoucher tourVoucher)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public ObservableCollection<ComplexRequestTour> GetAllAsCollection()
        {
            throw new System.NotImplementedException();
        }

        public int GetLastId()
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            using var selectCommand = databaseConnection.CreateCommand();
            selectCommand.CommandText = "SELECT MAX(Id) FROM ComplexTour";

            using var selectReader = selectCommand.ExecuteReader();
            return !selectReader.Read() ? 0 : selectReader.GetInt32(0);
        }
    }
}
