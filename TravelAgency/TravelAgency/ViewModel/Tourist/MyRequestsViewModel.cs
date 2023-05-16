using System.Collections.ObjectModel;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Tourist;

namespace TravelAgency.ViewModel.Tourist
{
    internal class MyRequestsViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;
        public RelayCommand NavigateToStatisticsCommand { get; set; }

        private ObservableCollection<RequestTour> _regularRequests;
        public ObservableCollection<RequestTour> RegularRequests
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
            var tourRequestService = new RequestTourService();
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;
            tourRequestService.UpdateAllRequestsStatuses();
            _regularRequests = tourRequestService.GetAllForSelectedYearAsCollection(null, CurrentUser.Username);
            NavigateToStatisticsCommand = new RelayCommand(Execute_NavigateToStatisticsCommand);
        }

        private void Execute_NavigateToStatisticsCommand(object parameter)
        {
            _navigationService.Navigate(new RequestsStatisticsView());
        }
    }
}
