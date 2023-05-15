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
        public RelayCommand NavigateToMyVouchersPageCommand { get; set; }
        public RelayCommand NavigateToMyRequestsCommand { get; set; }
        public RelayCommand NavigateToTourReservationPageCommand { get; set; }
        public RelayCommand NavigateToUserProfileCommand { get; set; }
        public RelayCommand NavigateToRequestTourPageCommand { get; set; }
        public RelayCommand NavigateToNotificationsPageCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }
        public string? Username { get; set; }
        private bool _isTooltipsSwitchToggled;

        public bool IsTooltipsSwitchToggled
        {
            get => _isTooltipsSwitchToggled;
            set
            {
                _isTooltipsSwitchToggled = value;
                OnPropertyChanged();
            }
        }

        public TouristViewModel(NavigationService navigationService)
        {
            _windowManager = new WindowManager();
            _navigationService = navigationService;

            NavigateToHomePageCommand = new RelayCommand(Execute_NavigateToHomePageCommand);
            NavigateToMyToursPageCommand = new RelayCommand(Execute_NavigateToMyToursPageCommand);
            NavigateToMyVouchersPageCommand = new RelayCommand(Execute_NavigateToMyVouchersPageCommand);
            NavigateToMyRequestsCommand = new RelayCommand(Execute_NavigateToMyRequestsCommand);
            NavigateToRequestTourPageCommand = new RelayCommand(Execute_NavigateToRequestTourPageCommand);
            NavigateToTourReservationPageCommand = new RelayCommand(Execute_NavigateToTourReservationPageCommand);
            NavigateToUserProfileCommand = new RelayCommand(Execute_NavigateToUserProfileCommand);
            NavigateToNotificationsPageCommand = new RelayCommand(Execute_NavigateToNotificationsPageCommand);
            CloseWindowCommand = new RelayCommand(Execute_CloseWindowCommand);
            SignOutCommand = new RelayCommand(Execute_SignOutCommand);

            Username = CurrentUser.DisplayName;
            _navigationService.Navigate(new HomeView(_navigationService, this));
        }

        private void Execute_NavigateToHomePageCommand(object parameter)
        {
            _navigationService.Navigate(new HomeView(_navigationService, this));
        }

        private void Execute_NavigateToMyToursPageCommand(object parameter)
        {
            _navigationService.Navigate(new MyToursView(_navigationService, this));
        }

        private void Execute_NavigateToMyVouchersPageCommand(object parameter)
        {
            _navigationService.Navigate(new MyTourVouchersView(this));
        }

        private void Execute_NavigateToMyRequestsCommand(object parameter)
        {
            _navigationService.Navigate(new MyRequestsView(_navigationService, this));
        }

        private void Execute_NavigateToTourReservationPageCommand(object parameter)
        {
            _navigationService.Navigate(new TourReservationView(_navigationService, this));
        }

        private void Execute_NavigateToUserProfileCommand(object parameter)
        {
            _navigationService.Navigate(new UserProfileView());
        }        
        
        private void Execute_NavigateToRequestTourPageCommand(object parameter)
        {
            _navigationService.Navigate(new RequestTourView(_navigationService, this));
        }       
        
        private void Execute_NavigateToNotificationsPageCommand(object parameter)
        {
            _navigationService.Navigate(new NotificationsView(_navigationService, this));
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
    }
}
