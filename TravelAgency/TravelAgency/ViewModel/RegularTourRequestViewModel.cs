using System;
using System.Diagnostics;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;

namespace TravelAgency.ViewModel
{
    internal class RegularTourRequestViewModel : BaseViewModel
    {
        #region Fields
        private readonly NavigationService _navigationService;
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
        #endregion

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

        private void Execute_SubmitRequestCommand(object parameter)
        {
            Debug.WriteLine(Country);
            Debug.WriteLine(City);
            Debug.WriteLine(Language);
            Debug.WriteLine(GuestNumber);
            Debug.WriteLine(StartingDate);
            Debug.WriteLine(EndingDate);
            Debug.WriteLine(Description);
        }

        private void Execute_CancelRequestCommand(object parameter)
        {
            _navigationService.GoBack();
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

        private bool IsDateRangeValid(DateTime? startingDate, DateTime? endingDate)
        {
            if (startingDate == null || endingDate == null) return false;
            return startingDate < endingDate;
        }

        public RegularTourRequestViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            _languages = Enum.GetValues(typeof(Language));
            SubmitRequestCommand = new RelayCommand(Execute_SubmitRequestCommand, CanExecute_SubmitRequestCommand);
            CancelRequestCommand = new RelayCommand(Execute_CancelRequestCommand, CanExecute_CancelRequestCommand);
        }

        public void SubmitTourRequest()
        {
            Debug.WriteLine(Country);
            Debug.WriteLine(City);
            Debug.WriteLine(Language);
            Debug.WriteLine(GuestNumber);
            Debug.WriteLine(StartingDate);
            Debug.WriteLine(EndingDate);
            Debug.WriteLine(Description);
        }
    }
}
