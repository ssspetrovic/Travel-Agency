using System;
using System.Diagnostics;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.View.Controls.Tourist;

namespace TravelAgency.ViewModel
{
    public class TouristViewModel
    {
        #region Fields and Properties
        private NavigationService NavigationService { get; set; }
        public RelayCommand NavigateToHomePageCommand { get; set; }
        public RelayCommand NavigateToMyToursPageCommand { get; set; }
        public RelayCommand NavigateToMyTourVouchersPageCommand { get; set; }
        public RelayCommand NavigateToRateTourPageCommand { get; set; }
        public RelayCommand NavigateToRequestTourPageCommand { get; set; }
        public RelayCommand NavigateToTourReservationPageCommand { get; set; }
        #endregion

        #region Actions
        private bool CanExecute_NavigationCommand(object? parameter)
        {
            return true;
        }

        private void Execute_NavigateToHomePageCommand(object? parameter)
        {
            NavigationService.Navigate(new HomeView());
        }

        private void Execute_NavigateToMyToursPageCommand(object? parameter)
        {
            NavigationService.Navigate(new MyToursView());
        }

        private void Execute_NavigateToRateTourPageCommand(object? parameter)
        {
            NavigationService.Navigate(new HomeView());
        }

        private void Execute_NavigateToMyTourVouchersPageCommand(object? parameter)
        {
            NavigationService.Navigate(new HomeView());
        }

        private void Execute_NavigateToRequestTourPageCommand(object? parameter)
        {
            NavigationService.Navigate(new HomeView());
        }

        private void Execute_NavigateToTourReservationPageCommand(object? parameter)
        {
            NavigationService.Navigate(new HomeView());
        }
        #endregion

        public TouristViewModel(NavigationService navigationService)
        {
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
        }
    }
}
