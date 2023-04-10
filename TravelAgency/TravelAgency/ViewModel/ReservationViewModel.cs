using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.DTO;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    internal class ReservationViewModel: BaseViewModel, INotifyPropertyChanged
    {
        private readonly CollectionViewSource _reservationCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private ReservationDTO _selectedReservations;
        private bool _isListViewShown;

        public ReservationViewModel() {

            var _reservationService = new ReservationService();

            _reservationCollection = new CollectionViewSource
            {
                Source = _reservationService.GetAllDTO()
            };
        }

        public ICollectionView ReservationSourceCollection => _reservationCollection.View;
        public ReservationDTO SelectedReservations
        {
            get => _selectedReservations;
            set
            {
                _selectedReservations = value;
                OnPropertyChanged();
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
    }
}
