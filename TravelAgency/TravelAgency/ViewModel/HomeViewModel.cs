using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Navigation;
using TravelAgency.DTO;
using TravelAgency.Repository;
using TravelAgency.Service;
using TravelAgency.Model;
using TravelAgency.Command;
using TravelAgency.WindowHelpers;
using TravelAgency.View;
using TravelAgency.View.Controls.Guest1;

namespace TravelAgency.ViewModel
{
    public class HomeViewModel: BaseViewModel, INotifyPropertyChanged
    {
        private AccommodationService _accommodationService = new();
        private ReservationService _reservationService = new();
        private LocationService _locationService = new();

        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;

        public new event PropertyChangedEventHandler? PropertyChanged;
        private CollectionViewSource _locationCollection;
        private CollectionViewSource _accommodationCollection;
        private AccommodationDTO _selectedAccommodation;
        private Location _selectedLocation;

        public RelayCommand ForumCommand { get; set; }
        public HomeViewModel(NavigationService navigationService)
        {
            _windowManager = new WindowManager();
            _navigationService = navigationService;

            _accommodationCollection = new CollectionViewSource
            {
                Source = _accommodationService.GetReservedAccommodations()
            };

            _locationCollection = new CollectionViewSource
            {
                Source = _locationService.GetAll()
            };

            ForumCommand = new RelayCommand(Exacute_ForumCommand);
        }
        public AccommodationDTO SelectedAccommodation
        {
            get
            {

                return _selectedAccommodation;
            }

            set
            {
                _selectedAccommodation = value;
                OnPropertyChanged();
            }

        }

        public Location SelectedLocation
        {
            get
            {

                return _selectedLocation;
            }

            set
            {
                _selectedLocation = value;
                OnPropertyChanged();
            }

        }

        public ICollectionView AccommodationSourceCollection => _accommodationCollection.View;
        public ICollectionView LocationSourceCollection => _locationCollection.View;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        public void Exacute_ForumCommand(object parameter)
        {
            _navigationService.Navigate(new ForumListView(_navigationService, this));
        }

    }
}
