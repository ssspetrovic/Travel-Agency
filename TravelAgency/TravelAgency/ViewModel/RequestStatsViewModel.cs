using System.Collections.Generic;
using System.Linq;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class RequestStatsViewModel : HomePageViewModel
    {
        private readonly RequestTourService _requestTourService;

        public RequestStatsViewModel()
        {
            _requestTourService = new RequestTourService();
        }

        public List<string> Locations => _requestTourService.GetAllRequestedLocations().Select(location => $"{location.City}, {location.Country}").ToList();
        public List<string> Languages => _requestTourService.GetAllRequestedLanguages();

    }
}
