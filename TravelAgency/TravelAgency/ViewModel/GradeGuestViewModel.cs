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

        public GradeGuestViewModel()
        {
            _tourRepository = new TourRepository();

            _toursCollection = new CollectionViewSource
            {
                Source = _tourRepository.GetAll()
            };
            _toursCollection.Filter += ToursCollection_Filter;

            if (!ToursSourceCollection.IsEmpty)
                IsListViewShown = true;

            IsTourSelected = false;
            _isGuestNumberEntered = false;
            _reservationRepository = new TourReservationRepository();
        }
    }
}
