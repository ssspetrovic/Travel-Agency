using System.Collections.ObjectModel;
using System.Linq;
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
        public RelayCommand NavigateToStatisticsCommand { get; set; }
        public RelayCommand ViewComplexRequestDetailsCommand { get; set; }

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

        private ObservableCollection<ComplexRequestTour> _complexRequests;
        public ObservableCollection<ComplexRequestTour> ComplexRequests
        {
            get => _complexRequests;
            set
            {
                _complexRequests = value;
                OnPropertyChanged();
            }
        }

        public MyRequestsViewModel(NavigationService navigationService)
        {
            var regularTourRequestService = new RequestTourService();
            var complexTourRequestService = new ComplexTourRequestService();
            _navigationService = navigationService;

            CheckForComplexStatusUpdate();

            _regularRequests = regularTourRequestService.GetAllForSelectedYearAsCollection(null, CurrentUser.Username);
            _regularRequests = new ObservableCollection<RequestTour>(
                _regularRequests.Select(request =>
                {
                    if (string.IsNullOrEmpty(request.AcceptedDate))
                    {
                        request.AcceptedDate = "/";
                    }
                    return request;
                }));

            _complexRequests = complexTourRequestService.GetAllAsCollection();

            NavigateToStatisticsCommand = new RelayCommand(Execute_NavigateToStatisticsCommand);
            ViewComplexRequestDetailsCommand = new RelayCommand(Execute_ViewComplexRequestDetailsCommand);
        }

        private void CheckForComplexStatusUpdate()
        {
            var complexRequestService = new ComplexTourRequestService();
            complexRequestService.HandleComplexStatuses();
        }

        private void Execute_NavigateToStatisticsCommand(object parameter)
        {
            _navigationService.Navigate(new RequestsStatisticsView());
        }

        private void Execute_ViewComplexRequestDetailsCommand(object parameter)
        {
            if (parameter is RequestTour tourPart)
            {
                _navigationService.Navigate(new ComplexTourRequestOverviewView(tourPart.Id));
            }
        }
    }
}
