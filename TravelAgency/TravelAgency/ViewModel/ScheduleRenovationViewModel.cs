using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class ScheduleRenovationViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _accommodationsCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;

        //private Reservation? _selectedReservation;
        //private bool _isReservatoinSelected;

        //private readonly ReservationRepository _reservationRepository;
        private readonly AccommodationService _accommodationService;
        public ICollectionView AccommodationsSourceCollection => _accommodationsCollection.View;
        public ScheduleRenovationViewModel()
        {
            _accommodationService = new AccommodationService();

            _accommodationsCollection = new CollectionViewSource
            {
                Source = _accommodationService.GetAccommodationNames()
            };
        }

        /*public bool IsReservationSelected
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
        }*/
    }
}
