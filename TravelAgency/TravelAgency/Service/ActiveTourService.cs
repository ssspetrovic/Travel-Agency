using Microsoft.Data.Sqlite;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class ActiveTourService : RepositoryBase
    {
        private readonly ActiveTourRepository _activeTourRepository;

        public ActiveTourService()
        {
            _activeTourRepository = new ActiveTourRepository();
        }


        public void UpdateKeyPoint(string keyPoint)
        {
            _activeTourRepository.UpdateKeyPoint(keyPoint);
        }

        public string GetActiveTour(string column)
        {
            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            var selectStatement = "select " + column + " from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            return selectReader.Read() ? selectReader.GetString(0) : "Error";
        }

        public void Remove()
        {
            _activeTourRepository.Remove();
        }

        public bool IsActive()
        {
            return _activeTourRepository.IsActive();
        }

        public void Add(ActiveTour activeTour)
        {
            _activeTourRepository.Add(activeTour);
        }
    }
}
