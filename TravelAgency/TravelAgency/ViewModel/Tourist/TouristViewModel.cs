using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.View;
using TravelAgency.View.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel.Tourist
{
    public class TouristViewModel : BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;
        public RelayCommand NavigateToHomePageCommand { get; set; }
        public RelayCommand NavigateToMyToursPageCommand { get; set; }
        public RelayCommand NavigateToMyTourVouchersPageCommand { get; set; }
        public RelayCommand NavigateToRateTourPageCommand { get; set; }
        public RelayCommand NavigateToRequestTourPageCommand { get; set; }
        public RelayCommand NavigateToTourReservationPageCommand { get; set; }
        public RelayCommand NavigateToUserProfileCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }
        public string? Username { get; set; }

        private void Execute_NavigateToHomePageCommand(object parameter)
        {
            _navigationService.Navigate(new HomeView(_navigationService));
        }

        private void Execute_NavigateToMyToursPageCommand(object parameter)
        {
            _navigationService.Navigate(new MyToursView(_navigationService));
        }

        private void Execute_NavigateToRateTourPageCommand(object parameter)
        {
            _navigationService.Navigate(new RateTourView(_navigationService));
        }

        private void Execute_NavigateToMyTourVouchersPageCommand(object parameter)
        {
            _navigationService.Navigate(new MyTourVouchersView());
        }

        private void Execute_NavigateToRequestTourPageCommand(object parameter)
        {
            _navigationService.Navigate(new RequestTourView(_navigationService));
        }

        private void Execute_NavigateToTourReservationPageCommand(object parameter)
        {
            _navigationService.Navigate(new TourReservationView(_navigationService));
        }

        private void Execute_NavigateToUserProfileCommand(object parameter)
        {
            _navigationService.Navigate(new UserProfileView());
        }

        private void Execute_CloseWindowCommand(object parameter)
        {
            _windowManager.CloseWindow<TouristView>();
        }

        private void Execute_SignOutCommand(object parameter)
        {
            _windowManager.ShowWindow<SignInView>();
            _windowManager.CloseWindow<TouristView>();
        }

        public TouristViewModel(NavigationService navigationService)
        {
            _windowManager = new WindowManager();
            _navigationService = navigationService;

            NavigateToHomePageCommand = new RelayCommand(Execute_NavigateToHomePageCommand);
            NavigateToMyToursPageCommand = new RelayCommand(Execute_NavigateToMyToursPageCommand);
            NavigateToRateTourPageCommand = new RelayCommand(Execute_NavigateToRateTourPageCommand);
            NavigateToMyTourVouchersPageCommand = new RelayCommand(Execute_NavigateToMyTourVouchersPageCommand);
            NavigateToRequestTourPageCommand = new RelayCommand(Execute_NavigateToRequestTourPageCommand);
            NavigateToTourReservationPageCommand = new RelayCommand(Execute_NavigateToTourReservationPageCommand);
            NavigateToUserProfileCommand = new RelayCommand(Execute_NavigateToUserProfileCommand);
            CloseWindowCommand = new RelayCommand(Execute_CloseWindowCommand);
            SignOutCommand = new RelayCommand(Execute_SignOutCommand);

            Username = CurrentUser.DisplayName;
        }
    }
}
