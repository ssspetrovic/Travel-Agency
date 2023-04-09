using Microsoft.Data.Sqlite;
using System.Linq;
using System.Windows.Forms;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class ActiveTourService : RepositoryBase
    {
        private readonly ActiveTourRepository _activeTourRepository;
        private readonly LocationService _locationService;

        public string CurrentKeyPoint { get; set; }

        public ActiveTourService()
        {
            _activeTourRepository = new ActiveTourRepository();
            _locationService = new LocationService();
            CurrentKeyPoint = "/";
        }


        public void UpdateKeyPoint(string currentKeyPoint)
        {
            currentKeyPoint = _locationService.GetByCity(currentKeyPoint)!.Id.ToString();

            using var databaseConnection = GetConnection();
            databaseConnection.Open();

            const string selectStatement = "select KeyPointsList from ActiveTour";
            using var selectCommand = new SqliteCommand(selectStatement, databaseConnection);
            using var selectReader = selectCommand.ExecuteReader();

            var keyPoints = "";

            if (selectReader.Read())
            {
                keyPoints = FindCurrentKeyPoint(currentKeyPoint, selectReader.GetString(0));
            }

            databaseConnection.Close();
            databaseConnection.Open();


            const string updateStatement = "update ActiveTour set KeyPointsList = $KeyPointsList";
            using var updateCommand = new SqliteCommand(updateStatement, databaseConnection);
            updateCommand.Parameters.AddWithValue("KeyPointsList", keyPoints);
            updateCommand.ExecuteNonQuery();

        }

        public string FindCurrentKeyPoint(string currentKeyPoint, string keyPoints)
        {
            var allKeyPoints = keyPoints.Split(", ").ToList();
            var lastKeyPoint = "";

            for (var i = 0; i < allKeyPoints.Capacity; i++)
            {
                var location = allKeyPoints[i];

                if (location.Contains(currentKeyPoint + ":True"))
                {
                    MessageBox.Show("We already passed " + _locationService.GetById(int.Parse(currentKeyPoint))!.City + "!");
                    break;
                }

                if (location.Contains(currentKeyPoint + ":False"))
                    if (lastKeyPoint.Contains(":True"))
                    {
                        allKeyPoints[i] = currentKeyPoint + ":True";
                        CurrentKeyPoint = currentKeyPoint;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("You can't skip key points!");
                        break;
                    }

                lastKeyPoint = allKeyPoints[i];
            }

            return string.Join(", ", allKeyPoints);
        }

        public string GetActiveTourColumn(string column)
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

        public bool ExistsByName(string name)
        {
            return _activeTourRepository.ExistsByName(name);
        }

        public string GetCurrentKeyPointByName(string name)
        {
            return GetActiveTourColumn(name);
        }
    }
}
