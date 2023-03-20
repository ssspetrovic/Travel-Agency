using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.View;
using TravelAgency.View.Controls.Tourist;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace TravelAgency.ViewModel
{
    public class TourReservationViewModel : BaseViewModel
    {
        private string? _filterText;
        private readonly CollectionViewSource _toursCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private bool _isFilteredCollectionEmpty;
        private bool _isListViewShown;
        private bool _shouldUpdateFilteredCollectionEmpty;
        private TourModel _selectedTour;
        private string? _guestNumber;
        private bool _isTourSelected;
        private bool _isGuestNumberEntered;

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
                _shouldUpdateFilteredCollectionEmpty = true;
                _toursCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
                OnPropertyChanged();
            }
        }

        public TourModel? SelectedTour
        {
            get => _selectedTour;

            set
            {
                _selectedTour = value;
                IsTourSelected = true;
                OnPropertyChanged();
                //Debug.WriteLine(_selectedTour.Name);
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
                RaisePropertyChanged("FilterText");
            }
        }
        public ICollectionView ToursSourceCollection => _toursCollection.View;

        public TourReservationViewModel()
        {
            var tourRepository = new TourRepository();

            _toursCollection = new CollectionViewSource
            {
                Source = tourRepository.GetAll()
            };
            _toursCollection.Filter += ToursCollection_Filter;

            if (!ToursSourceCollection.IsEmpty)
                IsListViewShown = true;

            IsTourSelected = false;
            _isGuestNumberEntered = false;
        }

        private bool DoGuestsFit(int maxGuests)
        {
            int.TryParse(GuestNumber, out var guestNumber);
            return guestNumber <= maxGuests;
        }

        private bool DoesLocationFit(LocationModel location)
        {
            return location.City.Equals(SelectedTour?.Location.City) &&
                   location.Country.Equals(SelectedTour?.Location.Country);
        }

        private void ToursCollection_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            // Checks if "tour = e.Item as Tour" is true
            if (e.Item is not TourModel tour) return;

            var filterTextUpper = FilterText.ToUpper();

            if (_isGuestNumberEntered)
            {
                //Debug.WriteLine("in");
                //Debug.Write("selected")
                //Debug.WriteLine($"{tour.Name}: {tour.MaxGuests} - entered guests {guestNumber}");
                //Debug.WriteLine("Tour location: " + tour.Location);
                //Debug.WriteLine("Selected location: " + SelectedTour?.Location);
                //Debug.WriteLine(tour.Location.City.Equals(SelectedTour?.Location.City) && tour.Location.Country.Equals(SelectedTour?.Location.Country));

                if (DoGuestsFit(tour.MaxGuests) && DoesLocationFit(tour.Location))
                {
                    //Debug.WriteLine($"accepted: {tour.Name}");
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            else
            {

                if (tour.Location.City.ToUpper().Contains(filterTextUpper) || tour.Location.Country.ToUpper().Contains(filterTextUpper) ||
                    tour.Duration.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(filterTextUpper) ||
                    tour.Language.ToString().ToUpper().Contains(filterTextUpper) || tour.MaxGuests.ToString().ToUpper().Contains(filterTextUpper))
                {
                    e.Accepted = true;
                }
                else
                {
                    //Debug.WriteLine($"declined: {tour.Name}");
                    e.Accepted = false;
                }
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
            {
                UpdateFilteredCollectionEmpty();
            }
        }

        public void MakeReservation()
        {
            if (SelectedTour == null)
            {
                MessageBox.Show("Error while selecting tour. Please try again.");
                return;
            }

            // SelectedTour contains the tour we selected in the moment of using button
            Debug.WriteLine(SelectedTour);
            if (!int.TryParse(GuestNumber, out var guestNumber))
            {
                MessageBox.Show("Invalid number of guests!");
            }

            if (guestNumber > SelectedTour.MaxGuests)
            {
                MessageBox.Show("No room for that amount on guests. Showing other available options on the same location.");
                _isGuestNumberEntered = true;
                _toursCollection.View.Refresh();
                FilterText = " ";
            }
            else if (guestNumber < SelectedTour.MaxGuests)
            {
                var dialog = new GuestNumberDialog
                {
                    Owner = Application.Current.MainWindow,
                    DataContext = this
                };

                dialog.ShowDialog();

            }

            IsTourSelected = false;
        }
    }
}
