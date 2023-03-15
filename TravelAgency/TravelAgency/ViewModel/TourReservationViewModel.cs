using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.Model;

namespace TravelAgency.ViewModel
{
    internal class TourReservationViewModel : BaseViewModel
    {
        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private LocationModel _location;

        public LocationModel Location
        {
            get => _location;
            set
            {
                _location = value; 
                OnPropertyChanged();
            }
        }

        private int _tourLength;

        public int TourLength
        {
            get => _tourLength;
            set
            {
                _tourLength = value;
                OnPropertyChanged();
            }
        }

        private Language _language;

        public Language Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged();
            }
        }

        private int _maxTourists;

        public int MaxTourists
        {
            get => _maxTourists;
            set
            {
                _maxTourists = value;
                OnPropertyChanged();
            }
        }

        public ICommand SubmitCommand { get; }

        public TourReservationViewModel()
        {
            
        }
    }
}
