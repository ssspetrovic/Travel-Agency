﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Tourist;
using static System.Windows.Application;

namespace TravelAgency.ViewModel.Tourist
{
    internal class MyToursViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private MyTourDto? _selectedTour;
        private readonly MyTourDtoService _myTourDtoService;
        public string? InvitationText { get; set; }
        public RelayCommand JoinTourCommand { get; set; }
        public RelayCommand RateTourCommand { get; set; }
        public ObservableCollection<MyTourDto> MyTours { get; set; }

        public MyTourDto? SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged();
            }
        }

        public MyToursViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            _myTourDtoService = new MyTourDtoService();
            CheckForInvitation();
            MyTours = _myTourDtoService.GetAllAsCollection();
            JoinTourCommand = new RelayCommand(Execute_JoinTourCommand, CanExecute_JoinTourCommand);
            RateTourCommand = new RelayCommand(Execute_RateTourCommand, CanExecute_RateTourCommand);
        }

        // TODO Test this method
        private void Execute_JoinTourCommand(object parameter)
        {
            _myTourDtoService.JoinTour(SelectedTour);
            _navigationService.Navigate(new MyToursView(_navigationService));
        }

        private void Execute_RateTourCommand(object parameter)
        {
            _navigationService.Navigate(new RateTourView(_navigationService, SelectedTour!.Name));
        }

        private bool CanExecute_JoinTourCommand(object parameter)
        {
            return _myTourDtoService.CanJoinTour(SelectedTour);
        }

        private bool CanExecute_RateTourCommand(object parameter)
        {
            return _myTourDtoService.CanRateTour(SelectedTour);
        }

        private void CheckForInvitation()
        {
            var touristService = new TouristService();
            var tourist = touristService.GetByUsername(CurrentUser.Username);

            if (tourist.TouristAppearance != TouristAppearance.Pinged) return;
            var tourName = tourist.Tour.Name;
            InvitationText = $"Guide is asking you to check in for '{tourName}' tour!";

            if (!IsAttendanceConfirmed()) return;
            touristService.UpdateAppearanceByUsername(CurrentUser.Username, TouristAppearance.Present);
            _myTourDtoService.UpdateStatus(tourName, MyTourDto.TourStatus.Attending);
        }

        private bool IsAttendanceConfirmed()
        {
            var dialog = new AcceptInvitationDialog()
            {
                Owner = Current.MainWindow,
                DataContext = this
            };

            dialog.ShowDialog();
            return AcceptInvitationDialog.ConfirmStatus;
        }

        public static void ReloadWindow()
        {
            Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = new TouristView
                {
                    ContentFrame =
                    {
                        Source = new Uri("Controls/Tourist/MyToursView.xaml", UriKind.Relative)
                    }
                };
                var currentWindow = Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                mainWindow.Show();
                currentWindow?.Close();
            });
        }
    }
}
