using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.View;
using TravelAgency.View.Controls.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class TouristViewModel
    {
        #region Fields and Properties
        private readonly IWindowManager _windowManager;
        private NavigationService NavigationService { get; set; }
        public RelayCommand NavigateToHomePageCommand { get; set; }
        public RelayCommand NavigateToMyToursPageCommand { get; set; }
        public RelayCommand NavigateToMyTourVouchersPageCommand { get; set; }
        public RelayCommand NavigateToRateTourPageCommand { get; set; }
        public RelayCommand NavigateToRequestTourPageCommand { get; set; }
        public RelayCommand NavigateToTourReservationPageCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }
        #endregion

        #region Actions
        private bool CanExecute_NavigationCommand(object parameter)
        {
            return true;
        }

        private void Execute_NavigateToHomePageCommand(object parameter)
        {
            NavigationService.Navigate(new HomeView());
        }

        private void Execute_NavigateToMyToursPageCommand(object parameter)
        {
            NavigationService.Navigate(new MyToursView());
        }

        private void Execute_NavigateToRateTourPageCommand(object parameter)
        {
            NavigationService.Navigate(new RateTourView());
        }

        private void Execute_NavigateToMyTourVouchersPageCommand(object parameter)
        {
            NavigationService.Navigate(new MyTourVouchersView());
        }

        private void Execute_NavigateToRequestTourPageCommand(object parameter)
        {
            NavigationService.Navigate(new RequestTourView());
        }

        private void Execute_NavigateToTourReservationPageCommand(object parameter)
        {
            NavigationService.Navigate(new TourReservationView());
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
        #endregion

        public TouristViewModel(IWindowManager windowManager, NavigationService navigationService)
        {
            _windowManager = windowManager;
            NavigationService = navigationService;
            NavigateToHomePageCommand =
                new RelayCommand(Execute_NavigateToHomePageCommand, CanExecute_NavigationCommand);
            NavigateToMyToursPageCommand =
                new RelayCommand(Execute_NavigateToMyToursPageCommand, CanExecute_NavigationCommand);
            NavigateToRateTourPageCommand =
                new RelayCommand(Execute_NavigateToRateTourPageCommand, CanExecute_NavigationCommand);
            NavigateToMyTourVouchersPageCommand =
                new RelayCommand(Execute_NavigateToMyTourVouchersPageCommand, CanExecute_NavigationCommand);
            NavigateToRequestTourPageCommand =
                new RelayCommand(Execute_NavigateToRequestTourPageCommand, CanExecute_NavigationCommand);
            NavigateToTourReservationPageCommand =
                new RelayCommand(Execute_NavigateToTourReservationPageCommand, CanExecute_NavigationCommand);
            CloseWindowCommand =
                new RelayCommand(Execute_CloseWindowCommand, CanExecute_NavigationCommand);
            SignOutCommand =
                new RelayCommand(Execute_SignOutCommand, CanExecute_NavigationCommand);
        }
    }
}
