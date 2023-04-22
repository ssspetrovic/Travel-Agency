using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class CreateSuggestedTourViewModel : GuideViewModel
    {
        private readonly RequestTourService _requestTourService;
        private readonly LocationService _locationService;

        public CreateSuggestedTourViewModel()
        {
            _requestTourService = new RequestTourService();
            _locationService = new LocationService();
        }

        public string MostRequestedLocation
        {
            get
            {
                var location =
                    _locationService.GetById(int.Parse(_requestTourService.GetMostRequestedStat("Location_Id")));
                return location!.City + ", " + location.Country;
            }
        }
        public string MostRequestedLanguage => _requestTourService.GetMostRequestedStat("Language");
    }
}
