using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    internal class AdvancedSearchViewModel:BaseViewModel, INotifyPropertyChanged
    {

        public new event PropertyChangedEventHandler? PropertyChanged;
        private DateTime _startDate;
        private string _name;
        private string _renter;
        private string? _location;
        private string _people;
        private readonly CollectionViewSource _locations;
        private string _days;


        public string Renter
        {
            get => _renter;
            set
            {
                if (_renter != value)
                {
                    _renter = value;

                }
                OnPropertyChanged();
            }
        }
        public string GuestNumber
        {
            get => _people;
            set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }


        public string SelectedLocation
        {
            get => _location;
            set
            {
                if (_location == value) return;
                _location = value;
                OnPropertyChanged();
            }
        }

        public string Days
        {
            get => _days;
            set
            {
                _days = value;
                OnPropertyChanged();
            }
        }

        public CollectionViewSource Locations
        {
            get => _locations;
        }

        public AdvancedSearchViewModel()
        {
            StartDate = DateTime.Now;
            var _locationService = new LocationService();
            _locations = new CollectionViewSource
            {
                Source = _locationService.GetAll()
            };
        }


        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public void Search()
        {
            MessageBox.Show(SelectedLocation + " " + GuestNumber);
        }

        public void Clear()
        {
            GuestNumber = string.Empty;
            SelectedLocation = string.Empty;
            Days = string.Empty;
            Name = string.Empty;
            Renter = string.Empty;
            RaisePropertyChanged("GuestNumber");
            RaisePropertyChanged("SelectedLocation");
            RaisePropertyChanged("Name");
            RaisePropertyChanged("Days");
            RaisePropertyChanged("Renter");
        }
    }
}
