﻿using System.Windows;
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

        public TourReservationService(TourReservationViewModel tourReservationViewModel)
        {
            _tourReservationViewModel = tourReservationViewModel;
            TourVoucherService = new TourVoucherService();
            TourService = new TourService();
            _tourReservationRepository = new TourReservationRepository();
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
            
            if (_tourReservationViewModel.SelectedTourVoucher != null)
                TourVoucherService.DeleteById(_tourReservationViewModel.SelectedTourVoucher.Id);

            _tourReservationViewModel.ReloadWindow();
        }
    }
}
