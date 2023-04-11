using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Shapes;
using TravelAgency.Repository;
using TravelAgency.View.Controls;
using TravelAgency.View.Controls.Owner;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        ReservationRepository reservationRepository = new ReservationRepository();
        public OwnerView()
        {
            InitializeComponent();
            int count = reservationRepository.CountReservationsToGrade();
            if(count > 0 )
            {
                lblGuestToGrade.Foreground = Brushes.DarkRed;
                lblGuestToGrade.Content = "Guests left to grade: " + count.ToString();
            }
            else
            {
                lblGuestToGrade.Foreground = Brushes.LightGreen;
                lblGuestToGrade.Content = "No guests to grade";
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void btnCreateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            var createAccommodationView = new CreateAccommodation();
            createAccommodationView.Show();
            Close();   
        }

        private void btnGradeGuest_Click(object sender, RoutedEventArgs e)
        {
            var gradeGuestView = new GradeGuest();
            gradeGuestView.Show();
            Close();
        }

        private void btnDisplayGuestsGrades_Click(object sender, RoutedEventArgs e)
        {
            var displayGuestsGrades = new DisplayGuestsGrades();
            displayGuestsGrades.Show();
            Close();
        }

        private void btnSuperOwner_Click(object sender, RoutedEventArgs e)
        {
            var superOwner = new SuperOwner();
            superOwner.Show();
            Close();
        }

        private void btnHandleRequests_Click(object sender, RoutedEventArgs e)
        {
            var reservationChangeRequests = new ReservationChangeRequestsView();
            reservationChangeRequests.Show();
            Close();
        }
    }
}
