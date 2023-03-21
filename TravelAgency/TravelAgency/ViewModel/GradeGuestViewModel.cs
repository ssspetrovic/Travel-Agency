using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    public class GradeGuestViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _reservationsCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private Reservation? _selectedReservation;
        private bool _isReservatoinSelected;

        private readonly ReservationRepository _reservationRepository;
        public ICollectionView ReservationsSourceCollection => _reservationsCollection.View;
        public GradeGuestViewModel()
        {
            _reservationRepository = new ReservationRepository();

            _reservationsCollection = new CollectionViewSource
            {
                Source = _reservationRepository.GetReservationsToGrade()
            };
            _reservationRepository = new ReservationRepository();
        }

        public bool IsReservationSelected
        {
            get => _isReservatoinSelected;
            set
            {
                _isReservatoinSelected = value;
                OnPropertyChanged();
            }
        }
        public Reservation? SelectedReservation
        {
            get => _selectedReservation;

            set
            {
                _selectedReservation = value;
                IsReservationSelected = true;
                OnPropertyChanged();
            }
        }
    }
}
