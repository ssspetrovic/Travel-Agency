using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TravelAgency.Service;
using TravelAgency.View;
using TravelAgency.View.Controls.Guide;

namespace TravelAgency.ViewModel
{
    public class RequestStatsViewModel : HomePageViewModel
    {
        private readonly RequestTourService _requestTourService;

        public RequestStatsViewModel()
        {
            _requestTourService = new RequestTourService();
        }


        public void Selected()
        {
            var currentRequestStatsViewModel = new CurrentRequestStatsViewModel();
            var currentRequestStats = new CurrentRequestStats(currentRequestStatsViewModel);
            currentRequestStats.Show();
            var currentWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault()!;
            currentWindow.Close();
        }

        public List<string> Locations => _requestTourService.GetAllRequestedLocations().Select(location => $"{location.City}, {location.Country}").ToList();
        public List<string> Languages => _requestTourService.GetAllRequestedLanguages();

    }
}
