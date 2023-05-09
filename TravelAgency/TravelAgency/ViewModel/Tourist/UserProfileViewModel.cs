using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.View;
using TravelAgency.View.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel.Tourist
{
    public enum NextView
    {
        Home,
        Notifications,
        MyTours,
        MyVouchers,
        MyRequests
    }

    internal class UserProfileViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly IWindowManager _windowManager;
        public RelayCommand NavigateToHomeCommand { get; set; }
        public RelayCommand NavigateToNotificationsCommand { get; set; }
        public RelayCommand NavigateToMyToursCommand { get; set; }
        public RelayCommand NavigateToMyVouchersCommand { get; set; }
        public RelayCommand NavigateToMyRequestsCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }
        public string? Username { get; set; }

        private void Execute_NavigateToHomeCommand(object parameter)
        {
            var window = new TouristView();
            window.Show();
            _windowManager.CloseWindow<UserProfileView>();
        }
        
        private void Execute_NavigateToNotificationsCommand(object parameter)
        {
            // TODO
        }
        
        private void Execute_NavigateToMyToursCommand(object parameter)
        {
            var window = new TouristView(NextView.MyTours);
            window.Show();
            _windowManager.CloseWindow<UserProfileView>();
        }

        private void Execute_NavigateToMyVouchersCommand(object parameter)
        {
            var window = new TouristView(NextView.MyVouchers);
            window.Show();
            _windowManager.CloseWindow<UserProfileView>();
        }

        private void Execute_NavigateToMyRequestsCommand(object parameter)
        {
            // TODO
        }

        private void Execute_SignOutCommand(object parameter)
        {
            _windowManager.ShowWindow<SignInView>();
            _windowManager.CloseWindow<UserProfileView>();
        }


        public UserProfileViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            _windowManager = new WindowManager();
            Username = CurrentUser.Username;

            NavigateToHomeCommand = new RelayCommand(Execute_NavigateToHomeCommand);
            NavigateToNotificationsCommand = new RelayCommand(Execute_NavigateToNotificationsCommand);
            NavigateToMyToursCommand = new RelayCommand(Execute_NavigateToMyToursCommand);
            NavigateToMyVouchersCommand = new RelayCommand(Execute_NavigateToMyVouchersCommand);
            NavigateToMyRequestsCommand = new RelayCommand(Execute_NavigateToMyRequestsCommand);
            SignOutCommand = new RelayCommand(Execute_SignOutCommand);
        }
    }
}
