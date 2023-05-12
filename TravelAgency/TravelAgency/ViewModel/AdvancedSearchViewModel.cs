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
        private Location? _location;
        private int _people;
        private readonly CollectionViewSource _locations;
        private int _days;


        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
            }
        }

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }


        public Location SelectedLocation
        {
            get => _location;
            set
            {
                _location = value;
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
    }
}
