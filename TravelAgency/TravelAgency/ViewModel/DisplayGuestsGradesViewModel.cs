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
using System.Data;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class DisplayGuestsGradesViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _guestsGradesCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private ReservationDisplayReviewsDTO? _selectedGuestsGrade;
        private bool _isGuestsGradeSelected;

        private readonly ReservationRepository _reservationRepository;
        private readonly ReservationService _reservationService;
        public ICollectionView GuestsGradesSourceCollection => _guestsGradesCollection.View;

        private string _guestName;
        private string _accommodationName;
        public DisplayGuestsGradesViewModel()
        {
            _reservationService = new ReservationService();
            AccommodationRepository _acc = new AccommodationRepository();

            _guestsGradesCollection = new CollectionViewSource
            {
                Source = _reservationService.GetGuestsGradesToDisplay()
            };

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
        
        public ReservationDisplayReviewsDTO? SelectedGuestsGrade
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

        private void SetNames(ReservationDisplayReviewsDTO? r)
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
