using System;
using System.Diagnostics;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Controls.Tourist;

namespace TravelAgency.ViewModel
{
    public class RegularTourRequestViewModel : BaseViewModel
    {
        #region Fields
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
        public RelayCommand NavigateToMyTourRequestsCommand { get; set; }
        private TourRequestAcceptedDialog? Dialog { get; set; }
        #endregion

        #region Properties
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
        #endregion

        #region Actions
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

            Dialog?.ShowDialog();
            Dialog = new TourRequestAcceptedDialog(this);
        }

        private void Execute_CancelRequestCommand(object parameter)
        {
            _navigationService.GoBack();
        }

        private void Execute_NavigateToMyTourRequestsCommand(object parameter)
        {
            Debug.WriteLine("IN");
            _navigationService.Navigate(new HomeView());
            Dialog?.Close();
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

        private bool CanExecute_CancelRequestCommand(object parameter)
        {
            return true;
        }
        #endregion

        private bool IsDateRangeValid(DateTime? startingDate, DateTime? endingDate)
        {
            if (startingDate <= DateTime.Now || startingDate == null || endingDate == null) return false;
            return startingDate < endingDate;
        }

        #region Constructors
        public RegularTourRequestViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            _tourRequestService = new RegularTourRequestService();
            _languages = Enum.GetValues(typeof(Language));
            SubmitRequestCommand =
                new RelayCommand(Execute_SubmitRequestCommand, CanExecute_SubmitRequestCommand);
            CancelRequestCommand =
                new RelayCommand(Execute_CancelRequestCommand, CanExecute_CancelRequestCommand);
            NavigateToMyTourRequestsCommand =
                new RelayCommand(Execute_NavigateToMyTourRequestsCommand, CanExecute_CancelRequestCommand);
        }
        #endregion
    }
}
