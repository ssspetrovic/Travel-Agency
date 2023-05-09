﻿using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.View;
using TravelAgency.View.Controls.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
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
            var window = _windowManager.GetWindow<TouristView>();
            window.TourReservationButton.IsChecked = true;
            _navigationService.Navigate(new TourReservationView(_navigationService));
        }

        private void Execute_NavigateToTourRequest(object parameter)
        {
            var window = _windowManager.GetWindow<TouristView>();
            window.RequestTourButton.IsChecked = true;
            _navigationService.Navigate(new RequestTourView(_navigationService));
        }
        
        // TODO Fix navigation when the Wizard View is implemented
        private void Execute_NavigateToHelpWizard(object parameter)
        {

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
