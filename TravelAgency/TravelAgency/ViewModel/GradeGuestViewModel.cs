using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.View.Controls.Owner;
using TravelAgency.View;
using TravelAgency.Service;
using TravelAgency.DTO;

namespace TravelAgency.ViewModel
{
    public class GradeGuestViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _reservationsCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private ReservationDisplayReviewsDTO? _selectedReservation;
        private bool _isReservatoinSelected;
        public RelayCommand BtnGradeGuest { get; set; }

        private object _selectedOption1;
        private object _selectedOption2;

        private string _comment;
        private string _lblResToGrade;
        private string _guestName;

        private readonly ReservationRepository _reservationRepository;
        private readonly ReservationService _reservationService;
        public ICollectionView ReservationsSourceCollection => _reservationsCollection.View;
        public GradeGuestViewModel()
        {
            BtnGradeGuest = new RelayCommand(Execute_BtnGradeGuest);
            _lblResToGrade = "(" + GetResToGrade() + ")";

            _reservationRepository = new ReservationRepository();
            _reservationService = new ReservationService();

            _reservationsCollection = new CollectionViewSource
            {
                Source = _reservationService.GetGuestsToGrade()
            };
        }

        private void Execute_BtnGradeGuest(object parameter)
        {
            if (IsReservationSelected && SelectedOption1 != null && SelectedOption2 != null)
            {
                ReservationRepository _reservationRepository = new ReservationRepository();
                int resId = SelectedReservation.Id;
                string comment = Comment;
                float gradeComplacent = Convert.ToInt32(SelectedOption1);
                float gradeClean = Convert.ToInt32(SelectedOption2);
                //_reservationRepository.UpdateReservationAfterGrading(resId, comment, gradeComplacent, gradeClean);
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("Guest graded successfully!", "Message");
                else
                    MessageBox.Show("Gost ocenjen uspešno!", "Poruka");
                Refresh();
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("You have not filled out all the information", "Message");
                else
                    MessageBox.Show("Niste ispunili sve potrebne informacije", "Poruka");
            }
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
        public ReservationDisplayReviewsDTO? SelectedReservation
        {
            get => _selectedReservation;

            set
            {
                _selectedReservation = value;
                IsReservationSelected = true;
                OnPropertyChanged();
                GuestName = GetGuestName();
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

        public string Comment
        {
            get => _comment;

            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        public string LblResToGrade
        {
            get => _lblResToGrade;

            set
            {
                _lblResToGrade = value;
                OnPropertyChanged();
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
            GradeGuestsView gradeGuestsView = new GradeGuestsView();
            mainFrame.Navigate(gradeGuestsView);
        }

        private string GetResToGrade()
        {
            ReservationRepository reservationRepository = new ReservationRepository();
            return reservationRepository.CountReservationsToGrade().ToString();
        }

        private string GetGuestName()
        {
            UserRepository userRepository = new UserRepository();
            User user = userRepository.GetById(SelectedReservation.GuestId);
            return user.Name + " " + user.Surname;
        }

    }
}
