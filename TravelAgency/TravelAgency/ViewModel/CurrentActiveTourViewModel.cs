using System.Collections.Generic;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class CurrentActiveTourViewModel : GuideViewModel
    {
        private readonly TouristService _touristService;
        private readonly ActiveTourService _activeTourService;
        private readonly LocationService _locationService;

        public CurrentActiveTourViewModel()
        {
            _touristService = new TouristService();
            _activeTourService = new ActiveTourService();
            _locationService = new LocationService();
        }

        public List<string> PassedKeyPoints
        {
            get
            {
                var keyPoints = _activeTourService.GetActiveTourColumn("KeyPointsList");
                var keyPointsList = keyPoints.Split(", ").ToList();
                var passedKeyPoints = new List<string>();

                foreach (var keyPoint in keyPointsList)
                {
                    var city = keyPoint.Split(":");
                    passedKeyPoints.Add(city[1]);
                }

                return passedKeyPoints;
            }
        }

        public string ActiveTourName => _activeTourService.GetActiveTourColumn("Name");

        public List<string> KeyPoints
        {
            get
            {
                var keyPoints = _activeTourService.GetActiveTourColumn("KeyPointsList");
                var keyPointsList = keyPoints.Split(", ").ToList();
                var locations = new List<Location?>();
                var cities = new List<string>();

                foreach (var keyPoint in keyPointsList)
                {
                    var city = keyPoint.Split(":");
                    locations.Add(_locationService.GetById(int.Parse(city[0])));
                }

                foreach (var location in locations)
                {
                    cities.Add(location?.City!);
                }

                return cities;
            }
        }

        public List<string> Tourists
        {
            get
            {
                var tourists = _activeTourService.GetActiveTourColumn("Tourists");
                var touristsList = tourists.Split(", ").ToList();
                return touristsList;
            }
        }

        public List<string> CheckedTourists
        {
            get
            {
                var tourists = _activeTourService.GetActiveTourColumn("Tourists");
                var touristsList = tourists.Split(", ").ToList();

                return touristsList.Select(tourist => _touristService.GetByUsername(tourist).TouristAppearance.ToString()).ToList();
            }
        }

        public List<string> StartingLocation
        {
            get
            {
                var tourists = _activeTourService.GetActiveTourColumn("Tourists");
                var touristsList = tourists.Split(", ").ToList();
                var locations = new List<string>();

                foreach (var tourist in touristsList)
                {
                    var currentTourist = _touristService.GetByUsername(tourist);
                    locations.Add(_locationService.GetById(currentTourist.LocationId)!.City);
                }

                return locations;
            }
        }
    }
}
