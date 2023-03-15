using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.Command;
using TravelAgency.Model;

namespace TravelAgency.ViewModel
{
    internal class MakeTourReservationViewModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
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

        private float _duration;

        public float Duration
        {
            get => _duration;
            set
            {
                _duration = value;
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

        public MakeTourReservationViewModel(TourModel tour)
        {
            SubmitCommand = new MakeTourReservationCommand(this, tour);
        }
    }
}
