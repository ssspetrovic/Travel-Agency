using System.Collections.ObjectModel;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.View.Controls.Tourist;
using TravelAgency.ViewModel;

namespace TravelAgency.Service
{
    public class TourReservationService
    {
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourReservationViewModel _tourReservationViewModel;

        public TourService TourService { get; }
        public TourVoucherService TourVoucherService { get; }

        public TourReservationService()
        {
            _tourReservationViewModel = new TourReservationViewModel();
            _tourReservationRepository = new TourReservationRepository();
            TourVoucherService = new TourVoucherService();
            TourService = new TourService();
        }

        public TourReservationService(TourReservationViewModel tourReservationViewModel)
        {
            _tourReservationViewModel = tourReservationViewModel;
            _tourReservationRepository = new TourReservationRepository();
            TourVoucherService = new TourVoucherService();
            TourService = new TourService();
        }

        private void CompleteReservation(int guestNumber)
        {
            if (_tourReservationViewModel.SelectedTour == null)
            {
                MessageBox.Show("Failed to complete reservation", "Error");
                return;
            }

            if (CurrentUser.Username == null || CurrentUser.DisplayName == null)
            {
                MessageBox.Show("Failed to fetch current user data!");
            }
            else
            {
                var myTourDtoService = new MyTourDtoService();
                myTourDtoService.Add(_tourReservationViewModel.SelectedTour);
                _tourReservationRepository.Add(new TourReservation(_tourReservationViewModel.SelectedTour.Id, _tourReservationViewModel.SelectedTour.Name, guestNumber, CurrentUser.Username, CurrentUser.DisplayName));
                
                MessageBox.Show("Reservation was successful!", "Success");
            }
        }

        private void HandleFinalGuestNumber(int finalGuestNumber)
        {
            if (finalGuestNumber <= 0)
            {
                MessageBox.Show("Failed to make a reservation! Invalid guest number!", "Error");
                _tourReservationViewModel.IsGuestNumberEntered = false;
            }
            else
            {
                if (_tourReservationViewModel.SelectedTourVoucher != null)
                {
                    if (!_tourReservationViewModel.SelectedTourVoucher.Description!.Contains("Valid"))
                    {
                        MessageBox.Show("Cannot use invalid voucher", "Error");
                        return;
                    }

                    TourVoucherService.DeleteById(_tourReservationViewModel.SelectedTourVoucher!.Id);
                }

                if (_tourReservationViewModel.SelectedTour == null) return;
                TourService.UpdateMaxGuests(_tourReservationViewModel.SelectedTour.Id, _tourReservationViewModel.SelectedTour.MaxGuests - finalGuestNumber);
                
                CompleteReservation(finalGuestNumber);
            }
        }

        private int CalculateFinalGuestNumber(int guestNumber, int maxGuests)
        {
            if (guestNumber > maxGuests) return -1;
            if (guestNumber == maxGuests) return guestNumber;

            var newGuestNumber = _tourReservationViewModel.GetDialogGuests();
            return GuestNumberDialog.IsUpdateConfirmed ? newGuestNumber : guestNumber;
        }

        public void MakeReservation()
        {
            var selectedTour = _tourReservationViewModel.SelectedTour;
            if (selectedTour?.MaxGuests == 0)
            {
                MessageBox.Show("The selected tour is full. Showing other available options on the same location.", "Tour full");
                _tourReservationViewModel.IsGuestNumberEntered = true;
                _tourReservationViewModel.FilterText = " ";
                return;
            }

            if (!int.TryParse(_tourReservationViewModel.GuestNumber, out var guestNumber))
            {
                MessageBox.Show("Invalid number of guests!", "Error");
                return;
            }

            var finalGuestNumber = selectedTour != null ? CalculateFinalGuestNumber(guestNumber, selectedTour.MaxGuests) : guestNumber;
            HandleFinalGuestNumber(finalGuestNumber);

            
            _tourReservationViewModel.ReloadWindow();
        }

        public Collection<TourReservation> GetAllAsCollection()
        {
            return _tourReservationRepository.GetAllAsCollection();
        }
    }
}
