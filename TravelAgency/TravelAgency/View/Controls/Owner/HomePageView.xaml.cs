using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for HomePageView.xaml
    /// </summary>
    public partial class HomePageView : Page
    {
        ReservationRepository reservationRepository = new ReservationRepository();
        DelayRequestRepository delayRequestRepository = new DelayRequestRepository();
        public HomePageView()
        {
            InitializeComponent();
            lblName.Content = CurrentUser.DisplayName;

            int gradeCount = reservationRepository.CountGradesForOwner(CurrentUser.Id);
            double averageGrade = reservationRepository.AverageGradeForOwner(CurrentUser.Id);

            lblNumReviews.Content = lblNumReviews.Content + " " + gradeCount.ToString();
            lblGradeAverage.Content = lblGradeAverage.Content + " " + averageGrade.ToString();

            if (gradeCount >= 50 && averageGrade >= 9.5)
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    lblTitle.Content = "Title: Super Owner";
                else
                    lblTitle.Content = "Titula: Super Vlasnik";
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    lblTitle.Content = "Title: Regular Owner";
                else
                    lblTitle.Content = "Titula: Običan Vlasnik";
            }

            int count1 = reservationRepository.CountReservationsToGrade();
            lblToGrade.Content = lblToGrade.Content + " " + count1.ToString();

            int count2 = getReservationChangeRequestCount();
            lblToChange.Content = lblToChange.Content + " " + count2.ToString();

            lblForums.Content = lblForums.Content + " 0"; 
        }

        private void btnGradeGuest_Click(object sender, RoutedEventArgs e)
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

        private void btnReservationChangeRequest_Click(object sender, RoutedEventArgs e)
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

        private int getReservationChangeRequestCount()
        {
            ObservableCollection<DelayRequest> delayRequests = delayRequestRepository.GetDelayRequests(CurrentUser.Id);
            int count = delayRequests.Count;
            return count;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
