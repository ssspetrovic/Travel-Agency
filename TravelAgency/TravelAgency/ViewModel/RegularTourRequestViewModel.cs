using System;
using System.Diagnostics;
using TravelAgency.Model;

namespace TravelAgency.ViewModel
{
    internal class RegularTourRequestViewModel : BaseViewModel
    {
        private string? _country;
        private string? _city;
        private Language? _language;
        private string? _guestNumber;
        private DateTime? _startingDate;
        private DateTime? _endingDate;
        private string? _description;
        private Array _languages;

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

        public RegularTourRequestViewModel()
        {
            _languages = Enum.GetValues(typeof(Language));
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
