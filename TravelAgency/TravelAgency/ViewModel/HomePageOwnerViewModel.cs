using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Command;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using TravelAgency.Service;
using TravelAgency.View.Tourist;
using System.Windows.Controls;
using TravelAgency.View.Controls.Owner;
using TravelAgency.View;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Repository;
using System.Collections.ObjectModel;

namespace TravelAgency.ViewModel
{
    public class HomePageOwnerViewModel : BaseViewModel
    {
        ReservationRepository _reservationRepository = new ReservationRepository();
        ReservationService _reservationService = new ReservationService();
        DelayRequestRepository _delayRequestRepository = new DelayRequestRepository();
        public RelayCommand BtnGradeGuest { get; set; }
        public RelayCommand BtnReservationChangeRequest { get; set; }
        public RelayCommand BtnAccSuggestion { get; set; }  

        private string _ownerName;
        private string _lblNumReviews;
        private string _lblGradeAverage;
        private string _lblTitle;
        private string _lblGuestsToGrade;
        private string _lblRequests;

        public HomePageOwnerViewModel() 
        {
            BtnGradeGuest = new RelayCommand(Execute_BtnGradeGuest);
            BtnReservationChangeRequest = new RelayCommand(Execute_BtnReservationChangeRequest);
            BtnAccSuggestion = new RelayCommand(Execute_BtnAccSuggestion);

            _ownerName = GetOwnerName();
            _lblNumReviews = GetNumReviews();
            _lblGradeAverage = GetGradeAverage();
            _lblTitle = GetTitle();
            _lblGuestsToGrade = GetGuestsToGrade();
            _lblRequests = GetRequests();
        }

        private void Execute_BtnGradeGuest(object parameter)
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

        private void Execute_BtnReservationChangeRequest(object parameter)
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
            AccommodationSuggestionView accommodationSuggestionView = new AccommodationSuggestionView();
            mainFrame.Navigate(accommodationSuggestionView);
        }

        private void Execute_BtnAccSuggestion(object parameter)
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
            AccommodationSuggestionView accommodationSuggestionView = new AccommodationSuggestionView();
            mainFrame.Navigate(accommodationSuggestionView);
        }
        public string OwnerName
        {
            get => _ownerName;
            set
            {
                _ownerName = value;
                OnPropertyChanged();
            }
        }

        public string LblNumReviews
        {
            get => _lblNumReviews;
            set
            {
                _lblNumReviews = value;
                OnPropertyChanged();
            }
        }

        public string LblGradeAverage
        {
            get => _lblGradeAverage;
            set
            {
                _lblGradeAverage = value;
                OnPropertyChanged();
            }
        }

        public string LblTitle
        {
            get => _lblTitle;
            set
            {
                _lblTitle = value;
                OnPropertyChanged();
            }
        }

        public string LblGuestsToGrade
        {
            get => _lblGuestsToGrade;
            set
            {
                _lblGuestsToGrade = value;
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

        private string GetOwnerName()
        {
            return CurrentUser.DisplayName;
        }

        private string GetNumReviews()
        {
            int gradeCount = _reservationRepository.CountGradesForOwner(CurrentUser.Id);
            return gradeCount.ToString();
        }

        private string GetGradeAverage()
        {
            double averageGrade = _reservationRepository.AverageGradeForOwner(CurrentUser.Id);
            return averageGrade.ToString();
        }

        private string GetTitle()
        {
            if (_reservationService.isSuperOwner(CurrentUser.Id))
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    return "Super Owner";
                else
                    return "Super Vlasnik";
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    return "Regular Owner";
                else
                    return "Običan Vlasnik";
            }
        }

        private string GetGuestsToGrade()
        {
            int count = _reservationRepository.CountReservationsToGrade();
            return count.ToString();
        }

        private string GetRequests()
        {
            ObservableCollection<DelayRequest> delayRequests = _delayRequestRepository.GetDelayRequests(CurrentUser.Id);
            int count = delayRequests.Count;
            return count.ToString();
        }
    }
}
