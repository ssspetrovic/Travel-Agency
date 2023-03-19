using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.View;

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

        public TourModel SelectedTour
        {
            get => _selectedTour;

            set
            {
                _selectedTour = value;
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

            if (tour.Location.City.ToUpper().Contains(filterTextUpper) || tour.Location.Country.ToUpper().Contains(filterTextUpper) ||
                tour.Duration.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(filterTextUpper) ||
                tour.Language.ToString().ToUpper().Contains(filterTextUpper) || tour.MaxGuests.ToString().ToUpper().Contains(filterTextUpper))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
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

        public static void MakeReservation(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Debug.WriteLine(button.Content);
            }
        }
    }
}
