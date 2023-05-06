using System;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.View.Controls.Tourist;

namespace TravelAgency.ViewModel
{
    internal class TouristViewModel
    {
        #region Fields and Properties
        public NavigationService NavigationService { get; set; }
        public RelayCommand NavigateToHomePageCommand { get; set; }
        public RelayCommand NavigateToMyToursPageCommand { get; set; }
        //public RelayCommand NavigateToMyTourVouchersPageCommand { get; set; }
        //public RelayCommand NavigateToRateTourPageCommand { get; set; }
        //public RelayCommand NavigateTRegularTourRequestPageCommand { get; set; }
        //public RelayCommand NavigateToRequestTourPageCommand { get; set; }
        //public RelayCommand NavigateToTourReservationPageCommand { get; set; }
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

        #endregion

        public TouristViewModel(NavigationService navigationService)
        {
            NavigationService = navigationService; 
            NavigateToHomePageCommand =
                new RelayCommand(Execute_NavigateToHomePageCommand, CanExecute_NavigationCommand);
            NavigateToMyToursPageCommand =
                new RelayCommand(Execute_NavigateToMyToursPageCommand, CanExecute_NavigationCommand);
        }
    }
}
