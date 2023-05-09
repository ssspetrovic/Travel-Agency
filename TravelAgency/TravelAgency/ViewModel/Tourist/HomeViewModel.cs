using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.View.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel.Tourist
{
    internal class HomeViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;
        public RelayCommand NavigateToBrowseToursCommand { get; set; }
        public RelayCommand NavigateToTourRequest { get; set; }
        public RelayCommand NavigateToHelpWizard { get; set; }

        private void Execute_NavigateToBrowseToursCommand(object parameter)
        {
            var window = _windowManager.GetTouristViewWindow();
            window.TourReservationButton.IsChecked = true;
            _navigationService.Navigate(new TourReservationView(_navigationService));
        }

        private void Execute_NavigateToTourRequest(object parameter)
        {
            var window = _windowManager.GetTouristViewWindow();
            window.RequestTourButton.IsChecked = true;
            _navigationService.Navigate(new RequestTourView(_navigationService));
        }

        private void Execute_NavigateToHelpWizard(object parameter)
        {
            // TODO Fix navigation when the Wizard View is implemented
        }

        public HomeViewModel(NavigationService navigationService)
        {
            _windowManager = new WindowManager();
            _navigationService = navigationService;
            NavigateToBrowseToursCommand = new RelayCommand(Execute_NavigateToBrowseToursCommand);
            NavigateToTourRequest = new RelayCommand(Execute_NavigateToTourRequest);
            NavigateToHelpWizard = new RelayCommand(Execute_NavigateToHelpWizard);
        }
    }
}
