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
using TravelAgency.DTO;

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

        private string _guestName;
        private string _accommodationName;
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

                SetNames(_selectedGuestsGrade);
            }
        }
        public string GuestName
        {
            get => _guestName;
            set
            {
                _guestName = value;
                OnPropertyChanged();
            }
        }

        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                _accommodationName = value;
                OnPropertyChanged();
            }
        }

        private void SetNames(Reservation? r)
        {
            UserRepository userRepository = new UserRepository();
            User u = userRepository.GetById(r.GuestId);
            GuestName = u.Name + " " + u.Surname;

            AccommodationRepository accommodationRepository = new AccommodationRepository();
            AccommodationDTO acc = accommodationRepository.GetById(r.AccommodationId);
            AccommodationName = acc.Name;
        }
    }
}
