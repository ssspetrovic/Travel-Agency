using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Controls.Tourist;
using static System.Windows.Application;

namespace TravelAgency.ViewModel
{
    public class TourReservationViewModel : BaseViewModel
    {
        private TourReservationView? _mainWindow;
        public new event PropertyChangedEventHandler? PropertyChanged;

        private readonly CollectionViewSource _toursCollection;
        private Array _filterLanguages;
        private ObservableCollection<TourVoucher> _tourVouchers;

        private string? _filterText;
        private bool _isFilteredCollectionEmpty;
        private bool _isListViewShown;
        private bool _shouldUpdateFilteredCollectionEmpty;
        private string? _enteredFilterCountry;
        private string? _enteredFilterCity;
        private Language? _selectedFilterLanguage;
        private int? _enteredFilterGuestNumber;
        private float? _enteredFilterDuration;

        private Tour? _selectedTour;
        private string? _guestNumber;
        private bool _isTourSelected;
        private string? _newGuestNumber;
        private string? _guestNumberText;
        private TourVoucher? _selectedTourVoucher;

        public ObservableCollection<TourVoucher> TourVouchers
        {
            get => _tourVouchers;
            set
            {
                _tourVouchers = value;
                OnPropertyChanged();
            }
        }

        public TourReservationService TourReservationService { get; set; }

        public bool IsGuestNumberEntered { get; set; }

        public TourVoucher? SelectedTourVoucher
        {
            get => _selectedTourVoucher;
            set
            {
                _selectedTourVoucher = value;
                OnPropertyChanged();
            }
        }

        public int? EnteredFilterGuestNumber
        {
            get => _enteredFilterGuestNumber;
            set
            {
                _enteredFilterGuestNumber = value;
                OnPropertyChanged();
            }
        }

        public float? EnteredFilterDuration
        {
            get => _enteredFilterDuration;
            set
            {
                _enteredFilterDuration = value;
                OnPropertyChanged();
            }
        }

        public Language? SelectedFilterLanguage
        {
            get => _selectedFilterLanguage;
            set
            {
                _selectedFilterLanguage = value;
                OnPropertyChanged();
            }
        }

        public string? EnteredFilterCountry
        {
            get => _enteredFilterCountry;
            set
            {
                _enteredFilterCountry = value;
                OnPropertyChanged();
            }
        }

        public string? EnteredFilterCity
        {
            get => _enteredFilterCity;
            set
            {
                _enteredFilterCity = value;
                OnPropertyChanged();
            }
        }

        public Array FilterLanguages
        {
            get => _filterLanguages;
            set
            {
                _filterLanguages = value;
                OnPropertyChanged();
            }
        }

        public string? NewGuestNumber
        {
            get => _newGuestNumber;
            set
            {
                _newGuestNumber = value;
                OnPropertyChanged();
            }
        }

        public string? GuestNumberText
        {
            get => _guestNumberText;
            set
            {
                _guestNumberText = value;
                OnPropertyChanged();
            }
        }

        public bool IsTourSelected
        {
            get => _isTourSelected;
            set
            {
                _isTourSelected = value;
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

        public Tour? SelectedTour
        {
            get => _selectedTour;

            set
            {
                _selectedTour = value;
                IsTourSelected = true;
                OnPropertyChanged();
            }
        }

        public bool IsFilteredCollectionEmpty
        {
            get => _isFilteredCollectionEmpty;
            set
            {
                if (_isFilteredCollectionEmpty == value) return;

                _isFilteredCollectionEmpty = value;
                OnPropertyChanged();
            }
        }

        public bool IsListViewShown
        {
            get => _isListViewShown;
            set
            {
                if (_isListViewShown == value) return;

                _isListViewShown = value;
                OnPropertyChanged();
            }
        }

        public string? FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                _shouldUpdateFilteredCollectionEmpty = true;
                _toursCollection.View.Refresh();
                RaisePropertyChanged(nameof(FilterText));
            }
        }
        public ICollectionView ToursSourceCollection => _toursCollection.View;

        public TourReservationViewModel()
        {
            TourReservationService = new TourReservationService(this);
            _toursCollection = new CollectionViewSource() { Source = TourReservationService.TourService.GetAllAsCollection() };
            _toursCollection.Filter += ToursCollection_Filter;
            _filterLanguages = Enum.GetValues(typeof(Language));
            _tourVouchers = TourReservationService.TourVoucherService.GetAllAsCollection();

            if (!ToursSourceCollection.IsEmpty)
                IsListViewShown = true;

            IsTourSelected = false;
            IsGuestNumberEntered = false;
            ResetFilter();
        }

        private bool IsLocationEqual(Location location)
        {
            return location.City.Equals(SelectedTour?.Location.City) &&
                   location.Country.Equals(SelectedTour?.Location.Country);
        }

        private bool IsMatchingFilterText(Tour tour, string filterTextUpper)
        {
            return tour.ToString().ToUpper().Contains(filterTextUpper) &&
                   (EnteredFilterCity == null || tour.Location.City.ToUpper().Contains(EnteredFilterCity.ToUpper())) &&
                   (EnteredFilterCountry == null || tour.Location.Country.ToUpper().Contains(EnteredFilterCountry.ToUpper())) &&
                   tour.Language.ToString().ToUpper().Contains(SelectedFilterLanguage?.ToString().ToUpper() ?? string.Empty) &&
                   Math.Abs(tour.Duration - (EnteredFilterDuration ?? tour.Duration)) < 0.01 &&
                   tour.MaxGuests == (EnteredFilterGuestNumber ?? tour.MaxGuests);
        }

        // Dynamic search filter trigger
        private void ToursCollection_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            // Checks if "tour = e.Item as Tour" is true
            if (e.Item is not Tour tour) return;

            if (IsGuestNumberEntered)
            {
                e.Accepted = IsLocationEqual(tour.Location) && tour.MaxGuests > 0;
            }
            else
            {
                e.Accepted = IsMatchingFilterText(tour, FilterText.ToUpper());
            }

            _shouldUpdateFilteredCollectionEmpty = true;
            IsFilteredCollectionEmpty = !e.Accepted && ToursSourceCollection.IsEmpty;
            IsListViewShown = !IsFilteredCollectionEmpty;
        }

        private void UpdateFilteredCollectionEmpty()
        {
            IsFilteredCollectionEmpty = ToursSourceCollection.IsEmpty;
            IsListViewShown = !IsFilteredCollectionEmpty;
            _shouldUpdateFilteredCollectionEmpty = false;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (_shouldUpdateFilteredCollectionEmpty)
                UpdateFilteredCollectionEmpty();
        }

        public int GetDialogGuests()
        {
            var dialog = new GuestNumberDialog
            {
                Owner = Current.MainWindow,
                DataContext = this
            };

            GuestNumberText =
                $"Tour can't take that many guests. Number of spaces left: {SelectedTour?.MaxGuests}";

            dialog.ShowDialog();

            if (int.TryParse(NewGuestNumber, out var newGuestNumber) && newGuestNumber <= SelectedTour?.MaxGuests)
                return newGuestNumber;

            return -1;
        }

        public void ApplyFilter()
        {
            FilterText = " ";
            RaisePropertyChanged(nameof(FilterText));
            _toursCollection.View.Refresh();
        }

        public void ResetFilter()
        {
            EnteredFilterCity = null;
            EnteredFilterCountry = null;
            SelectedFilterLanguage = null;
            EnteredFilterDuration = null;
            EnteredFilterGuestNumber = null;
            RaisePropertyChanged(nameof(FilterText));
            _toursCollection.View.Refresh();
        }

        // Called to reload window after the reservation was made
        public void ReloadWindow()
        {
            Current.Dispatcher.Invoke(() =>
            {
                _mainWindow = new TourReservationView();
                var currentWindow = Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                _mainWindow.Show();
                currentWindow?.Close();
            });
        }
    }
}
