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
using TravelAgency.Model;
using TravelAgency.View.Controls.Owner;
using TravelAgency.ViewModel;

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
            btnLightTheme.Visibility = Visibility.Visible;
            btnDarkTheme.Visibility = Visibility.Hidden;
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
            lblSelectedTab.Content = btnHomePage.Content;
        }

        private void btnRegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            RegisterAccommodationView registerAccommodationView = new RegisterAccommodationView();
            mainFrame.Navigate(registerAccommodationView);
            lblSelectedTab.Content = btnRegisterAccommodation.Content;
        }

        private void btnReservationChangeRequest_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            ReservationChangeRequestView reservationChangeRequestView = new ReservationChangeRequestView();
            mainFrame.Navigate(reservationChangeRequestView);
            lblSelectedTab.Content = btnReservationChangeRequest.Content;
        }

        private void btnGradeGuest_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            GradeGuestsView gradeGuestsView = new GradeGuestsView();
            mainFrame.Navigate(gradeGuestsView);
            lblSelectedTab.Content = btnGradeGuest.Content;
        }

        private void btnDisplayGuestReviews_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            DisplayGuestReviewsView displayGuestReviewsView = new DisplayGuestReviewsView();
            mainFrame.Navigate(displayGuestReviewsView);
            lblSelectedTab.Content = btnDisplayGuestReviews.Content;
        }

        private void btnLanguage_Click(object sender, RoutedEventArgs e)
        {
            var resourceDictionary = new ResourceDictionary();
            if (CurrentLanguageAndTheme.languageId == 1)
            {
                resourceDictionary.Source = new Uri($"/Resources/Styles/EnglishDictionary.xaml", UriKind.Relative);
                CurrentLanguageAndTheme.languageId = 0;
            }
            else
            {
                resourceDictionary.Source = new Uri($"/Resources/Styles/SerbianDictionary.xaml", UriKind.Relative);
                CurrentLanguageAndTheme.languageId = 1;
            }
            Application.Current.Resources.MergedDictionaries[0] = resourceDictionary;
        }

        private void btnLightTheme_Click(object sender, RoutedEventArgs e)
        {
            var resourceDictionary = new ResourceDictionary();
            if (CurrentLanguageAndTheme.themeId == 1)
            {
                resourceDictionary.Source = new Uri($"/Resources/Styles/LightTheme.xaml", UriKind.Relative);
                CurrentLanguageAndTheme.themeId = 0;
            }
            else
            {
                resourceDictionary.Source = new Uri($"/Resources/Styles/DarkTheme.xaml", UriKind.Relative);
                CurrentLanguageAndTheme.themeId = 1;
            }
            Application.Current.Resources.MergedDictionaries[1] = resourceDictionary;
            btnLightTheme.Visibility = Visibility.Hidden;
            btnDarkTheme.Visibility = Visibility.Visible;
        }

        private void btnDarkTheme_Click(object sender, RoutedEventArgs e)
        {
            var resourceDictionary = new ResourceDictionary();
            if (CurrentLanguageAndTheme.themeId == 1)
            {
                resourceDictionary.Source = new Uri($"/Resources/Styles/LightTheme.xaml", UriKind.Relative);
                CurrentLanguageAndTheme.themeId = 0;
            }
            else
            {
                resourceDictionary.Source = new Uri($"/Resources/Styles/DarkTheme.xaml", UriKind.Relative);
                CurrentLanguageAndTheme.themeId = 1;
            }
            Application.Current.Resources.MergedDictionaries[1] = resourceDictionary;
            btnLightTheme.Visibility = Visibility.Visible;
            btnDarkTheme.Visibility = Visibility.Hidden;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void btnScheduleRenovation_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            ScheduleRenovationView scheduleRenovationView = new ScheduleRenovationView();
            mainFrame.Navigate(scheduleRenovationView);
            lblSelectedTab.Content = btnScheduleRenovation.Content;
        }

        private void btnRenovationOverview_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility = Visibility.Hidden;
            RenovationOverviewView renovationOverviewView = new RenovationOverviewView();
            mainFrame.Navigate(renovationOverviewView);
            lblSelectedTab.Content = btnRenovationOverview.Content;
        }
    }
}
