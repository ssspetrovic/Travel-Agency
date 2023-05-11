using System;
using System.ComponentModel;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Tourist;
using static System.Windows.Application;

namespace TravelAgency.ViewModel.Tourist
{
    public class RegularTourRequestViewModel : BaseViewModel
    {
        private readonly TouristViewModel _touristViewModel;
        private readonly NavigationService _navigationService;
        private readonly RegularTourRequestService _tourRequestService;
        private string? _country;
        private string? _city;
        private Language? _language;
        private string? _guestNumber;
        private DateTime? _startingDate;
        private DateTime? _endingDate;
        private string? _description;
        private Array _languages;
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

        public string? Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        public string? City
        {
            get => _city;
            set
            {
                _city = value;
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
            _tourRequestService = new RegularTourRequestService();
            _languages = Enum.GetValues(typeof(Language));
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
            _tourRequestService.Add(new RegularTourRequest(
                CurrentUser.Username,
                new Location(City!, Country!),
                Language,
                int.Parse(GuestNumber!),
                $"{StartingDate?.ToString("yyyy-MM-dd")} - {EndingDate?.ToString("yyyy-MM-dd")}",
                Description!,
                RegularTourRequest.TourRequestStatus.OnHold));

            Dialog = new OkDialog
            {
                Owner = Current.MainWindow,
                Label =
                {
                    Content = "Request successfully created!"
                },
                Button =
                {
                    Command = new RelayCommand(Execute_NavigateToMyTourRequestsCommand)
                }
            };
            Dialog?.ShowDialog();
        }

        private void Execute_CancelRequestCommand(object parameter)
        {
            _navigationService.GoBack();
        }

        // TODO Change navigation when the view is implemented
        private void Execute_NavigateToMyTourRequestsCommand(object parameter)
        {
            Dialog?.Close();
            _navigationService.Navigate(new RequestTourView(_navigationService, _touristViewModel));
        }

        private bool CanExecute_SubmitRequestCommand(object parameter)
        {
            return !string.IsNullOrEmpty(Country) &&
                   !string.IsNullOrEmpty(City) &&
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
