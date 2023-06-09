using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View;
using TravelAgency.View.Controls.Guest1;
using TravelAgency.WindowHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TravelAgency.ViewModel
{
    public class GuestViewModel: BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;
        public RelayCommand NavigateToHomePageCommand { get; set; }
        public RelayCommand NavigateToMyAccommodationsCommand { get; set; }
        public RelayCommand NavigateToReservationsCommand { get; set; }
        public RelayCommand NavigateToRequestsCommand { get; set; }
        public RelayCommand NavigateToProfileCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }

        public GuestViewModel(NavigationService navigationService)
        {
            _windowManager = new WindowManager();
            _navigationService = navigationService;

            NavigateToHomePageCommand = new RelayCommand(Execute_NavigateToHomePageCommand);
            NavigateToMyAccommodationsCommand = new RelayCommand(Execute_NavigateToMyAccommodationsCommand);
            NavigateToReservationsCommand = new RelayCommand(Execute_NavigateToReservationsCommand);
            NavigateToRequestsCommand = new RelayCommand(Execute_NavigateToRequestsCommand);
            NavigateToProfileCommand = new RelayCommand(Execute_NavigateToProfileCommand);
            CloseWindowCommand = new RelayCommand(Execute_CloseWindowCommand);
            SignOutCommand = new RelayCommand(Execute_SignOutCommand);

            _navigationService.Navigate(new HomeView(_navigationService, this));
        }

        private void Execute_NavigateToHomePageCommand(object parameter)
        {
            _navigationService.Navigate(new HomeView(_navigationService, this));
        }

        private void Execute_NavigateToMyAccommodationsCommand(object parameter)
        {
            _windowManager.ShowWindow<AccommodationReservationView>();
            _windowManager.CloseWindow<Guest1View>();
        }

        private void Execute_NavigateToReservationsCommand(object parameter)
        {
            _windowManager.ShowWindow<ReservatoinView>();
            _windowManager.CloseWindow<Guest1View>();
        }

        private void Execute_NavigateToRequestsCommand(object parameter)
        {
            _windowManager.ShowWindow<RequestListView>();
            _windowManager.CloseWindow<Guest1View>();
        }

        private void Execute_NavigateToProfileCommand(object parameter)
        {
            _windowManager.ShowWindow<ProfileView>();
            _windowManager.CloseWindow<Guest1View>();
        }

        private void Execute_CloseWindowCommand(object parameter)
        {
            _windowManager.CloseWindow<Guest1View>();
        }

        private void Execute_SignOutCommand(object parameter)
        {
            _windowManager.ShowWindow<SignInView>();
            _windowManager.CloseWindow<Guest1View>();
        }

    }
}
