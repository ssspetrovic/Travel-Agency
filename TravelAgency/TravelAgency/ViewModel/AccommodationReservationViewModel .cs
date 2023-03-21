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
using TravelAgency.DTO;

namespace TravelAgency.ViewModel
{
    public class AccommodationReservationViewModel : BaseViewModel
    {
        private string? _filterText;
        private readonly CollectionViewSource _accommodationCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private bool _isFilteredCollectionEmpty;
        private bool _isListViewShown;
        private bool _shouldUpdateFilteredCollectionEmpty;
        private Accommodation _selectedAccommodation;

        public Accommodation SelectedTour
        {
            get => _selectedAccommodation;

            set
            {
                _selectedAccommodation = value;
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
                _accommodationCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
            }
        }
        public ICollectionView ToursSourceCollection => _accommodationCollection.View;

        public AccommodationReservationViewModel()
        {
            var accommodationRepository = new AccommodationRepository();

            _accommodationCollection = new CollectionViewSource
            {
                Source = accommodationRepository.GetAll()
            };
            _accommodationCollection.Filter += ToursCollection_Filter;

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

            if (e.Item is not AccommodationDTO accommodation) return;

            var filterTextUpper = FilterText.ToUpper();

            if (accommodation.Name.ToUpper().Contains(filterTextUpper) || accommodation.Location.City.ToUpper().Contains(filterTextUpper) || accommodation.Location.Country.ToUpper().Contains(filterTextUpper) ||
                accommodation.Type.ToString().ToUpper().Contains(filterTextUpper) ||
                accommodation.MaxReservationDays.ToString().Contains(filterTextUpper))
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
