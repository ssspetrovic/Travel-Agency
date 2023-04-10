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
    public class DisplayGuestsGradesViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _guestsGradesCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private Reservation? _selectedGuestsGrade;
        private bool _isGuestsGradeSelected;

        private readonly ReservationRepository _reservationRepository;
        public ICollectionView GuestsGradesSourceCollection => _guestsGradesCollection.View;
        public DisplayGuestsGradesViewModel()
        {
            _reservationRepository = new ReservationRepository();

            _guestsGradesCollection = new CollectionViewSource
            {
                Source = _reservationRepository.GetGuestsGradesToDisplay()
            };
            _reservationRepository = new ReservationRepository();
        }

        public bool IsGuestsGradeSelected
        {
            get => _isGuestsGradeSelected;
            set
            {
                _isGuestsGradeSelected = value;
                OnPropertyChanged();
            }
        }
        public Reservation? SelectedGuestsGrade
        {
            get => _selectedGuestsGrade;

            set
            {
                _selectedGuestsGrade = value;
                IsGuestsGradeSelected = true;
                OnPropertyChanged();
            }
        }
    }
}
