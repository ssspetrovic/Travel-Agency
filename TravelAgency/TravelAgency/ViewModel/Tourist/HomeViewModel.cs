﻿using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.View.Tourist;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel.Tourist
{
    internal class HomeViewModel : BaseViewModel
    {
        private readonly TouristViewModel _touristViewModel;
        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;

        public RelayCommand NavigateToBrowseToursCommand { get; set; }
        public RelayCommand NavigateToTourRequest { get; set; }
        public RelayCommand NavigateToHelpWizard { get; set; }
        public RelayCommand NavigateToRequestsStatisticsCommand { get; set; }

        private bool? _isToolTipSwitchToggled;
        public bool? IsToolTipSwitchToggled
        {
            get => _isToolTipSwitchToggled;
            set
            {
                _isToolTipSwitchToggled = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _touristViewModel = touristViewModel;
            _windowManager = new WindowManager();
            _navigationService = navigationService;
            NavigateToBrowseToursCommand = new RelayCommand(Execute_NavigateToBrowseToursCommand);
            NavigateToTourRequest = new RelayCommand(Execute_NavigateToTourRequest);
            NavigateToRequestsStatisticsCommand = new RelayCommand(Execute_NavigateToRequestsStatisticsCommand);
            NavigateToHelpWizard = new RelayCommand(Execute_NavigateToHelpWizard);
        }
        
        private void Execute_NavigateToBrowseToursCommand(object parameter)
        {
            var window = _windowManager.GetWindow<TouristView>();
            window.TourReservationButton.IsChecked = true;
            _navigationService.Navigate(new TourReservationView(_navigationService, _touristViewModel));
        }

        private void Execute_NavigateToTourRequest(object parameter)
        {
            var window = _windowManager.GetWindow<TouristView>();
            window.RequestTourButton.IsChecked = true;
            _navigationService.Navigate(new RequestTourView(_navigationService, _touristViewModel));
        }

        private void Execute_NavigateToRequestsStatisticsCommand(object parameter)
        {
            var window = _windowManager.GetWindow<TouristView>();
            window.RequestTourButton.IsChecked = true;
            _navigationService.Navigate(new RequestsStatisticsView());
        }

        private void Execute_NavigateToHelpWizard(object parameter)
        {
            var wizardDialog = new WizardDialogView
            {
                Owner = System.Windows.Application.Current.MainWindow
            };

            wizardDialog.Show();
        }
    }
}
