using System.Collections.Generic;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class TourViewModel
    {
        public Tour Tour { get; set; }
        public string KeyPoints { get; set; }

        public TourViewModel(Tour tour)
        {
            Tour = tour;
            KeyPoints = GetFormattedKeyPoints(tour.KeyPoints);
        }

        private string GetFormattedKeyPoints(List<Location?> keyPoints)
        {
            var locationService = new LocationService();
            return keyPoints.Aggregate(string.Empty, (current, location) => current + " - " + locationService.GetById(location!.Id)! + "\r\n");
        }
    }
}
