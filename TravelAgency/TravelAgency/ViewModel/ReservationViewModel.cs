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


namespace TravelAgency.ViewModel
{
    internal class ReservationViewModel: BaseViewModel, INotifyPropertyChanged
    {
        private readonly CollectionViewSource _reservationCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private List<Reservation> _selectedReservations;
        private bool _isListViewShown;

        public ReservationViewModel() {

            var _reservationRepository = new ReservationRepository();

            _reservationCollection = new CollectionViewSource
            {
                Source = _reservationRepository.GetAll()
            };
        }

        public ICollectionView ReservationSourceCollection => _reservationCollection.View;
        public List<Reservation> SelectedReservations
        {
            get => _selectedReservations;
            set
            {

                foreach (var reservation in value)
                {
                    _selectedReservations.Add(reservation);
                }
                
                /////////TODO//////////
                foreach(var el in value)
                {
                    MessageBox.Show(el.Id.ToString());
                }
                /////////////

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
