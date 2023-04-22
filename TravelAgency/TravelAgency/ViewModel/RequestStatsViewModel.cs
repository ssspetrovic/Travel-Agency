using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class RequestStatsViewModel : GuideViewModel
    {
        private readonly LocationService _locationService;

        public RequestStatsViewModel()
        {
            _locationService = new LocationService();
        }

        public List<string> Locations => _locationService.GetAll().Select(location => $"{location.City}, {location.Country}").ToList();
        public List<string> Languages => Enum.GetNames(typeof(Language)).ToList();

    }
}
