using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.View.Controls.Owner;
using TravelAgency.View;
using System.Windows;
using System.Collections.ObjectModel;
using Org.BouncyCastle.Asn1;
using TravelAgency.DTO;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class ReservationChangeRequestsViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _reservationRequestCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private DelayRequestDTO? _selectedRequest;
        private bool _isRequestSelected;
        public RelayCommand BtnAccept { get; set; }
        public RelayCommand BtnReject { get; set; }

        private string _rejectReason;
        private string _available;
        private string _lblRequests;
        private string _lblAccName;

        private readonly DelayRequestRepository _delayRequestRepository;
        private readonly DelayRequestService _delayRequestService;
        public ICollectionView ReservationRequestSourceCollection => _reservationRequestCollection.View;
        public ReservationChangeRequestsViewModel()
        {
            BtnAccept = new RelayCommand(Execute_BtnAccept);
            BtnReject = new RelayCommand(Execute_BtnReject);
            _lblRequests = "(" + getReservationChangeRequestCount() + ")";

            _delayRequestRepository = new DelayRequestRepository();
            _delayRequestService = new DelayRequestService();

            _reservationRequestCollection = new CollectionViewSource
            {
                Source = _delayRequestService.GetDelayRequests(CurrentUser.Id)
            };
            _delayRequestRepository = new DelayRequestRepository();
        }

        private void Execute_BtnAccept(object parameter)
        {
            if(IsRequestSelected)
            {
                if (string.IsNullOrWhiteSpace(RejectReason))
                {
                    ReservationRepository reservationRepository = new ReservationRepository();
                    DelayRequestRepository delayRequestRepository = new DelayRequestRepository();
                    AccommodationActivityRepository accommodationActivityRepository = new AccommodationActivityRepository();

                    int reservationId = SelectedRequest.ReservationId;
                    DateTime newStartDate = SelectedRequest.NewStartDate;
                    DateTime newEndDate = SelectedRequest.NewEndDate;
                    reservationRepository.AcceptReservationChangeRequest(reservationId, newStartDate, newEndDate);
                    delayRequestRepository.AcceptDelayRequest(reservationId);

                    DateTime oldStartDate = SelectedRequest.OldStartDate;
                    AccommodationActivity a = new AccommodationActivity(SelectedRequest.AccommodationId, oldStartDate, 0, 1, 0);
                    accommodationActivityRepository.Add(a);

                    if (CurrentLanguageAndTheme.languageId == 0)
                        MessageBox.Show("Reservation change request accepted successfully!", "Message");
                    else
                        MessageBox.Show("Zahtev za izmenu rezervacije uspeštno prihvaćen!", "Poruka");
                    Refresh();
                }
                else
                {
                    if (CurrentLanguageAndTheme.languageId == 0)
                        MessageBox.Show("Cannot accept and have a reject reason!", "Message");
                    else
                        MessageBox.Show("Ne možete prihvatiti zahtev, a imati razlog za odbijanje!", "Poruka");
                }
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("You need to select a request first...", "Message");
                else
                    MessageBox.Show("Morate prvo zahtev izabrati...", "Poruka");
            }
        }

        private void Execute_BtnReject(object parameter)
        {
            if(IsRequestSelected)
            {
                DelayRequestRepository delayRequestRepository = new DelayRequestRepository();

                int reservationId = SelectedRequest.ReservationId;
                delayRequestRepository.RejectDelayRequest(reservationId, RejectReason);
                MessageBox.Show(RejectReason);
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("Reservation change request rejected successfully!", "Message");
                else
                    MessageBox.Show("Zahtev za izmenu rezervacije uspeštno odbijen!", "Poruka");
                Refresh();
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("You need to select a request first...", "Message");
                else
                    MessageBox.Show("Morate prvo zahtev izabrati...", "Poruka");
            }
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
        public DelayRequestDTO? SelectedRequest
        {
            get => _selectedRequest;

            set
            {
                _selectedRequest = value;
                IsRequestSelected = true;
                OnPropertyChanged();
                Available = GetAvailable();
                AccName = GetAccName();
            }
        }

        public string RejectReason
        {
            get => _rejectReason;
            set
            {
                _rejectReason = value;
                OnPropertyChanged();
            }
        }

        public string Available
        {
            get => _available;
            set
            {
                _available = value;
                OnPropertyChanged();
            }
        }

        public string LblRequests
        {
            get => _lblRequests;
            set
            {
                _lblRequests = value;
                OnPropertyChanged();
            }
        }

        public string AccName
        {
            get => _lblAccName;
            set
            {
                _lblAccName = value;
                OnPropertyChanged();
            }
        }

        private void Refresh()
        {
            OwnerMainView mainWindow = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window is OwnerMainView)
                {
                    mainWindow = (OwnerMainView)window;
                    break;
                }
            }

            Frame mainFrame = mainWindow.mainFrame;
            ReservationChangeRequestView reservationChangeRequestView = new ReservationChangeRequestView();
            mainFrame.Navigate(reservationChangeRequestView);
        }

        private string GetAvailable()
        {
            ReservationRepository reservationRepository = new ReservationRepository();

            ObservableCollection<Reservation> reservationList = reservationRepository.GetAll();
            bool available = true;
            foreach (Reservation reservation in reservationList)
            {
                if (reservation.Id != SelectedRequest.ReservationId)
                {
                    if (reservation.AccommodationId == SelectedRequest.AccommodationId)
                    {
                        DateTime newStartDate = SelectedRequest.NewStartDate;
                        DateTime newEndDate = SelectedRequest.NewEndDate;
                        if (reservation.StartDate >= newStartDate && reservation.StartDate <= newEndDate)
                        {
                            available = false; break;
                        }
                        if (reservation.EndDate >= newStartDate && reservation.EndDate <= newEndDate)
                        {
                            available = false; break;
                        }
                    }
                }
            }
            if (available)
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    return "Available!";
                else
                    return "Slobodno!";
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    return "Not available!";
                else
                    return "Nije slobodno!";
            }
        }

        private int getReservationChangeRequestCount()
        {
            DelayRequestRepository delayRequestRepository = new DelayRequestRepository();
            ObservableCollection<DelayRequest> delayRequests = delayRequestRepository.GetDelayRequests(CurrentUser.Id);
            int count = delayRequests.Count;
            return count;
        }

        private string GetAccName()
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            AccommodationDTO accommodation = accommodationRepository.GetById(SelectedRequest.AccommodationId);
            return accommodation.Name;
        }
    }
}
