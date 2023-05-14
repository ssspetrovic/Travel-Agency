using System.Collections.ObjectModel;
using System.Windows.Navigation;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class MyRequestsViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;
        private ObservableCollection<RegularTourRequest> _regularRequests;

        public ObservableCollection<RegularTourRequest> RegularRequests
        {
            get => _regularRequests;
            set
            {
                _regularRequests = value;
                OnPropertyChanged();
            }
        }

        public MyRequestsViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            var tourRequestService = new RegularTourRequestService();
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;
            _regularRequests = tourRequestService.GetAllForSelectedYearAsCollection(null);
        }
    }
}
