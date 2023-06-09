using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using TravelAgency.DTO;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    internal class SearchResultsViewModel: BaseViewModel, INotifyPropertyChanged
    {
        private ReservationService _reservationService = new();
        public new event PropertyChangedEventHandler? PropertyChanged;
        private CollectionViewSource _collection;
        private AdvancedSearchViewModel _searchViewModel;


        public ICollectionView Collection => _collection.View;

        public SearchResultsViewModel(AdvancedSearchViewModel advancedSearchViewModel)
        {
            _searchViewModel = advancedSearchViewModel;
            var _accommodationService = new AccommodationService();
            _collection = new CollectionViewSource
            {
                Source = _accommodationService.GetAllForSearchResult()
            };
            _collection.Filter += Collection_Filter;
        }

        public CollectionViewSource CollectionView
        {
            get => _collection;
            set
            {
                _collection = value;
                OnPropertyChanged();
            }
        }

        private void Collection_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = true;
            if (e.Item is not AccommodationForSearch accommodation) return;
            if (string.IsNullOrEmpty(_searchViewModel.Name) || string.IsNullOrEmpty(_searchViewModel.SelectedLocation) || string.IsNullOrEmpty(_searchViewModel.Days)) return;

            if (string.IsNullOrEmpty(_searchViewModel.Days) || _searchViewModel.StartDate == null)
            {
                if ((accommodation.Name.ToUpper().Contains(_searchViewModel.Name.ToUpper()) || 
                    accommodation.Location.City.ToUpper().Contains(_searchViewModel.SelectedLocation.ToUpper()) || 
                    accommodation.Location.Country.ToUpper().Contains(_searchViewModel.SelectedLocation.ToUpper())) &&
                    accommodation.MaxGuest<int.Parse(_searchViewModel.GuestNumber) &&
                    accommodation.MinGuest>int.Parse(_searchViewModel.GuestNumber))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            else {
                if ((accommodation.Name.ToUpper().Contains(_searchViewModel.Name.ToUpper()) ||
                    accommodation.Location.City.ToUpper().Contains(_searchViewModel.SelectedLocation.ToUpper()) ||
                    accommodation.Location.Country.ToUpper().Contains(_searchViewModel.SelectedLocation.ToUpper())) &&
                    _reservationService.IsReservationValid(_searchViewModel.StartDate, _searchViewModel.StartDate.AddDays(int.Parse(_searchViewModel.Days)), accommodation.ReservableDays) &&
                    accommodation.MaxGuest < int.Parse(_searchViewModel.GuestNumber) &&
                    accommodation.MinGuest > int.Parse(_searchViewModel.GuestNumber))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }

        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
