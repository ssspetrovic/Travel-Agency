using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private readonly TouristViewModel _touristViewModel;
        private readonly NavigationService _navigationService;
        private MyTourDto? _selectedTour;
        private readonly MyTourDtoService _myTourDtoService;
        public string? InvitationText { get; set; }
        public RelayCommand JoinTourCommand { get; set; }
        public RelayCommand RateTourCommand { get; set; }
        public ObservableCollection<MyTourDto> MyTours { get; set; }
        private OkDialog? Dialog { get; set; }
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

        public MyTourDto? SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged();
            }
        }

        public MyToursViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _touristViewModel = touristViewModel;
            IsTooltipsSwitchToggled = _touristViewModel.IsTooltipsSwitchToggled;
            _touristViewModel.PropertyChanged += TouristViewModel_PropertyChanged;
            _navigationService = navigationService;
            _myTourDtoService = new MyTourDtoService();
            CheckForInvitation();
            MyTours = _myTourDtoService.GetAllAsCollection();
            JoinTourCommand = new RelayCommand(Execute_JoinTourCommand, CanExecute_JoinTourCommand);
            RateTourCommand = new RelayCommand(Execute_RateTourCommand, CanExecute_RateTourCommand);
        }

        private void TouristViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(TouristViewModel.IsTooltipsSwitchToggled)) return;
            if (sender != null) IsTooltipsSwitchToggled = ((TouristViewModel)sender).IsTooltipsSwitchToggled;
        }

        private void Execute_JoinTourCommand(object parameter)
        {
            _myTourDtoService.JoinTour(SelectedTour);
            Dialog = new OkDialog
            {
                Owner = Current.MainWindow,
                Label =
                {
                    Content = "Successfully joined the tour."
                },
                Button =
                {
                    Command = new RelayCommand(Execute_NavigateToMyToursCommand)
                }
            };
            Dialog?.Show();
        }

        private void Execute_RateTourCommand(object parameter)
        {
            _navigationService.Navigate(new RateTourView(_navigationService, _touristViewModel, SelectedTour!.Name));
        }

        private void Execute_NavigateToMyToursCommand(object parameter)
        {
            Dialog?.Close();
            _navigationService.Navigate(new MyToursView(_navigationService, _touristViewModel));
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
            var dialog = new AcceptInvitationDialog
            {
                Owner = Current.MainWindow,
                DataContext = this
            };

            dialog.ShowDialog();
            return AcceptInvitationDialog.ConfirmStatus;
        }
    }
}
