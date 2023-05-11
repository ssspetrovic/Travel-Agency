using System;
using System.Collections.Generic;
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
using TravelAgency.View.Controls.Owner;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for OwnerMainView.xaml
    /// </summary>
    public partial class OwnerMainView : Window
    {
        public OwnerMainView()
        {
            InitializeComponent();
            HomePageView homePageView = new HomePageView();
            mainFrame.Navigate(homePageView);
            stekPanel.Visibility = Visibility.Hidden;
        }

        private void mainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility= Visibility.Visible;
        }

        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            HomePageView homePageView = new HomePageView();
            mainFrame.Navigate(homePageView);
            lblSelectedTab.Content = "Home Page";
        }

        private void btnRegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            RegisterAccommodationView registerAccommodationView = new RegisterAccommodationView();
            mainFrame.Navigate(registerAccommodationView);
            lblSelectedTab.Content = "Register Accommodation";
        }

        private void btnReservationChangeRequest_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            ReservationChangeRequestView reservationChangeRequestView = new ReservationChangeRequestView();
            mainFrame.Navigate(reservationChangeRequestView);
            lblSelectedTab.Content = "Reservation Change Requests";
        }

        private void btnGradeGuest_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            GradeGuestsView gradeGuestsView = new GradeGuestsView();
            mainFrame.Navigate(gradeGuestsView);
            lblSelectedTab.Content = "Grade Guests";
        }

        private void btnDisplayGuestReviews_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            DisplayGuestReviewsView displayGuestReviewsView = new DisplayGuestReviewsView();
            mainFrame.Navigate(displayGuestReviewsView);
            lblSelectedTab.Content = "Display Guest Reviews";
        }
    }
}
