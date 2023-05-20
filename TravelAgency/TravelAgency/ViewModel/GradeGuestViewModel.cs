using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TravelAgency.Command;
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
        public RelayCommand BtnGradeGuest { get; set; }

        private bool _radioButtonValue1;
        private bool _radioButtonValue2;
        private object _selectedOption1;
        private object _selectedOption2;

        private readonly ReservationRepository _reservationRepository;
        public ICollectionView ReservationsSourceCollection => _reservationsCollection.View;
        public GradeGuestViewModel()
        {
            BtnGradeGuest = new RelayCommand(Execute_BtnGradeGuest);

            _reservationRepository = new ReservationRepository();

            _reservationsCollection = new CollectionViewSource
            {
                Source = _reservationRepository.GetReservationsToGrade()
            };
            _reservationRepository = new ReservationRepository();
        }

        private void Execute_BtnGradeGuest(object parameter)
        {
            MessageBox.Show(SelectedOption1.ToString());
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

        public bool RadioButtonValue1
        {
            get => _radioButtonValue1;

            set
            {
                _radioButtonValue1 = value;
                OnPropertyChanged();
            }
        }

        public bool RadioButtonValue2
        {
            get => _radioButtonValue2;

            set
            {
                _radioButtonValue2 = value;
                OnPropertyChanged();
            }
        }

        public object SelectedOption1
        {
            get => _selectedOption1;

            set
            {
                _selectedOption1 = value;
                OnPropertyChanged();
            }
        }
        public object SelectedOption2
        {
            get => _selectedOption2;

            set
            {
                _selectedOption2 = value;
                OnPropertyChanged();
            }
        }

    }
}
