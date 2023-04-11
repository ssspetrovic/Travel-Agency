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
    public class ReservationChangeRequestsViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _reservationRequestCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private DelayRequest? _selectedRequest;
        private bool _isRequestSelected;

        private readonly DelayRequestRepository _delayRequestRepository;
        public ICollectionView ReservationRequestSourceCollection => _reservationRequestCollection.View;
        public ReservationChangeRequestsViewModel()
        {
            _delayRequestRepository = new DelayRequestRepository();

            _reservationRequestCollection = new CollectionViewSource
            {
                Source = _delayRequestRepository.GetDelayRequests(CurrentUser.Id)
            };
            _delayRequestRepository = new DelayRequestRepository();
        }

        public bool IsRequestSelected
        {
            get => _isRequestSelected;
            set
            {
                _isRequestSelected = value;
                OnPropertyChanged();
            }
        }
        public DelayRequest? SelectedRequest
        {
            get => _selectedRequest;

            set
            {
                _selectedRequest = value;
                IsRequestSelected = true;
                OnPropertyChanged();
            }
        }
    }
}
