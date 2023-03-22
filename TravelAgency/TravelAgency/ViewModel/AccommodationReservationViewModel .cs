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
using TravelAgency.Service;
using System.Runtime.CompilerServices;

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
        private AccommodationDTO _selectedAccommodation;
        private DateTime _startDate;
        private DateTime _endDate;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public AccommodationDTO SelectedAccommodation
        {
            get {

                return _selectedAccommodation;
            }

            set
            {
                _selectedAccommodation = value;
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
                _accommodationCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
            }
        }
        public ICollectionView AccommodationSourceCollection => _accommodationCollection.View;

        public AccommodationReservationViewModel()
        {
            var accommodationRepository = new AccommodationRepository();

            _accommodationCollection = new CollectionViewSource
            {
                Source = accommodationRepository.GetAll()
            };
            _accommodationCollection.Filter += AccommodationCollection_Filter;

            if (!AccommodationSourceCollection.IsEmpty) 
                IsListViewShown = true;
        }


        private void AccommodationCollection_Filter(object sender, FilterEventArgs e)
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
            IsFilteredCollectionEmpty = !e.Accepted && AccommodationSourceCollection.IsEmpty;
            IsListViewShown = !IsFilteredCollectionEmpty;
        }

        private void UpdateFilteredCollectionEmpty()
        {
            IsFilteredCollectionEmpty = AccommodationSourceCollection.IsEmpty;
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

        public void MakeReservation(object sender, RoutedEventArgs e, DateTime? endDaySelect, DateTime? startDaySelect, AccommodationDTO accommodationDTO)
        {

            var _reservationService = new ReservationService();
            if (!_reservationService.IsReservationValid(endDaySelect, startDaySelect, accommodationDTO.MaxReservationDays))
            {

                MessageBox.Show("The reservation is out of bounds!");
            }
            else if(_reservationService.Reserve(endDaySelect, startDaySelect, accommodationDTO))
            {
                MessageBox.Show("Accommodation Reserved");
            }
            else
            {
                MessageBox.Show("Try this date: " + _reservationService.FindDate(endDaySelect, startDaySelect, accommodationDTO).AddDays(1).ToString());
            }
            
        }

    }
}
