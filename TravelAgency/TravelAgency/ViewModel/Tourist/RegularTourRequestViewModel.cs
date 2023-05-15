using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Tourist;

namespace TravelAgency.ViewModel.Tourist
{
    public class RegularTourRequestViewModel : BaseViewModel
    {
        private readonly TouristViewModel _touristViewModel;
        private readonly NavigationService _navigationService;
        private readonly RequestTourService _requestTourService;
        private Language? _language;
        private string? _guestNumber;
        private DateTime? _startingDate;
        private DateTime? _endingDate;
        private string? _description;
        private Array _languages;
        private ObservableCollection<Location> _locationsCollection;
        private Location? _selectedLocation;
        public RelayCommand SubmitRequestCommand { get; set; }
        public RelayCommand CancelRequestCommand { get; set; }
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

        public Location? SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Location> LocationsCollection
        {
            get => _locationsCollection;
            set
            {
                _locationsCollection = value;
                OnPropertyChanged();
            }
        }

        public Language? Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged();
            }
        }

        public string? GuestNumber
        {
            get => _guestNumber;
            set
            {
                _guestNumber = value;
                OnPropertyChanged();
            }
        }

        public DateTime? StartingDate
        {
            get => _startingDate;
            set
            {
                _startingDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? EndingDate
        {
            get => _endingDate;
            set
            {
                _endingDate = value;
                OnPropertyChanged();
            }
        }

        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public Array Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                OnPropertyChanged();
            }
        }

        public RegularTourRequestViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _touristViewModel = touristViewModel;
            IsTooltipsSwitchToggled = _touristViewModel.IsTooltipsSwitchToggled;
            _touristViewModel.PropertyChanged += TouristViewModel_PropertyChanged;
            _navigationService = navigationService;
            _requestTourService = new RequestTourService();
            _languages = Enum.GetValues(typeof(Language));
            _selectedLocation = null;
            var locationService = new LocationService();
            _locationsCollection = locationService.GetAll();
            SubmitRequestCommand = new RelayCommand(Execute_SubmitRequestCommand, CanExecute_SubmitRequestCommand);
            CancelRequestCommand = new RelayCommand(Execute_CancelRequestCommand);
        }
        
        private void TouristViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(TouristViewModel.IsTooltipsSwitchToggled)) return;
            if (sender != null) IsTooltipsSwitchToggled = ((TouristViewModel)sender).IsTooltipsSwitchToggled;
        }

        private void Execute_SubmitRequestCommand(object parameter)
        {
            _requestTourService.Add(new RequestTour(
                SelectedLocation!,
                Description!,
                Language!,
                int.Parse(GuestNumber!),
                $"{StartingDate?.ToString("MM/dd/yyyy")} - {EndingDate?.ToString("MM/dd/yyyy")}",
                Status.OnHold,
                CurrentUser.Username
            ));

            Dialog = new OkDialog
            {
                Owner = Application.Current.MainWindow,
                Label =
                {
                    Content = "Request successfully created!"
                },
                Button =
                {
                    Command = new RelayCommand(Execute_NavigateToMyRequestsCommand)
                }
            };
            Dialog?.ShowDialog();
        }

        private void Execute_CancelRequestCommand(object parameter)
        {
            _navigationService.GoBack();
        }

        private void Execute_NavigateToMyRequestsCommand(object parameter)
        {
            Dialog?.Close();
            _navigationService.Navigate(new MyRequestsView(_navigationService, _touristViewModel));
        }

        private bool CanExecute_SubmitRequestCommand(object parameter)
        {
            return SelectedLocation != null &&
                   Language != null &&
                   IsDateRangeValid(StartingDate, EndingDate) &&
                   int.TryParse(GuestNumber, out _) &&
                   !string.IsNullOrEmpty(Description);
        }

        private bool IsDateRangeValid(DateTime? startingDate, DateTime? endingDate)
        {
            if (startingDate < DateTime.Today || startingDate == null || endingDate == null) return false;
            return startingDate < endingDate;
        }
    }
}
