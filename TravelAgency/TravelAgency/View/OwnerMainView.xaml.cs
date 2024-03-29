﻿using System;
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
            HideMenu();
        }

        private void mainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            stekPanel.Visibility= Visibility.Visible;
            menuRectangle.Visibility = Visibility.Visible;
            btnBack.Visibility = Visibility.Visible;
            btnMenu.Visibility = Visibility.Hidden;
            icCalendar.Visibility = Visibility.Visible;
            icComments.Visibility = Visibility.Visible;
            icFilePen.Visibility = Visibility.Visible;
            icHome.Visibility = Visibility.Visible;
            icMagni.Visibility = Visibility.Visible;
            icPencil.Visibility = Visibility.Visible;
            icSignOut.Visibility = Visibility.Visible;
            icStat.Visibility = Visibility.Visible;
            icUserCheck.Visibility = Visibility.Visible;
        }

        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            HideMenu();
            HomePageView homePageView = new HomePageView();
            mainFrame.Navigate(homePageView);
            lblSelectedTab.Content = btnHomePage.Content;
        }

        private void btnRegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            HideMenu();
            RegisterAccommodationView registerAccommodationView = new RegisterAccommodationView();
            mainFrame.Navigate(registerAccommodationView);
            lblSelectedTab.Content = btnRegisterAccommodation.Content;
        }

        private void btnReservationChangeRequest_Click(object sender, RoutedEventArgs e)
        {
            HideMenu();
            ReservationChangeRequestView reservationChangeRequestView = new ReservationChangeRequestView();
            mainFrame.Navigate(reservationChangeRequestView);
            lblSelectedTab.Content = btnReservationChangeRequest.Content;

            ListView RequestListView = reservationChangeRequestView.RequestListView;
            ChangeColorListView(RequestListView);
        }

        private void btnGradeGuest_Click(object sender, RoutedEventArgs e)
        {
            HideMenu();
            GradeGuestsView gradeGuestsView = new GradeGuestsView();
            mainFrame.Navigate(gradeGuestsView);
            lblSelectedTab.Content = btnGradeGuest.Content;

            ListView GuestListView = gradeGuestsView.GuestListView;
            ChangeColorListView(GuestListView);
        }

        private void btnDisplayGuestReviews_Click(object sender, RoutedEventArgs e)
        {
            HideMenu();
            DisplayGuestReviewsView displayGuestReviewsView = new DisplayGuestReviewsView();
            mainFrame.Navigate(displayGuestReviewsView);
            lblSelectedTab.Content = btnDisplayGuestReviews.Content;

            ListView GuestListView = displayGuestReviewsView.GuestListView;
            ChangeColorListView(GuestListView);
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
            HideMenu();
            ScheduleRenovationView scheduleRenovationView = new ScheduleRenovationView();
            mainFrame.Navigate(scheduleRenovationView);
            lblSelectedTab.Content = btnScheduleRenovation.Content;
            ListView FreeDatesListView = scheduleRenovationView.FreeDatesListView;
            ChangeColorListView(FreeDatesListView);
            ComboBox cmbAccName = scheduleRenovationView.cmbAccName;
            ChangeColorComboBox(cmbAccName);
        }

        private void btnRenovationOverview_Click(object sender, RoutedEventArgs e)
        {
            HideMenu();
            RenovationOverviewView renovationOverviewView = new RenovationOverviewView();
            mainFrame.Navigate(renovationOverviewView);
            lblSelectedTab.Content = btnRenovationOverview.Content;

            ListView PreviousRenovationsListView = renovationOverviewView.PreviousRenovationsListView;
            ListView FutureRenovationsListView = renovationOverviewView.FutureRenovationsListView;
            ChangeColorListView(PreviousRenovationsListView);
            ChangeColorListView(FutureRenovationsListView);
        }

        private void btnAccommodationStatistics_Click(object sender, RoutedEventArgs e)
        {
            HideMenu();
            AccommodationStatisticsView accommodationStatisticsView = new AccommodationStatisticsView();
            mainFrame.Navigate(accommodationStatisticsView);
            lblSelectedTab.Content = btnAccommodationStatistics.Content;
        }

        private void HideMenu()
        {
            stekPanel.Visibility = Visibility.Hidden;
            menuRectangle.Visibility = Visibility.Hidden;
            btnMenu.Visibility = Visibility.Visible;
            btnBack.Visibility = Visibility.Hidden;
            icCalendar.Visibility = Visibility.Hidden;
            icComments.Visibility = Visibility.Hidden;
            icFilePen.Visibility = Visibility.Hidden;
            icHome.Visibility = Visibility.Hidden;
            icMagni.Visibility = Visibility.Hidden;
            icPencil.Visibility = Visibility.Hidden;
            icSignOut.Visibility = Visibility.Hidden;
            icStat.Visibility = Visibility.Hidden;
            icUserCheck.Visibility = Visibility.Hidden;
        }

        private void ChangeColorListView(ListView list)
        {
            if (CurrentLanguageAndTheme.themeId == 0)
            {
                list.Background = Brushes.White;
                list.Foreground = Brushes.Black;
            }
            else
            {
                list.Background = Brushes.Black;
                list.Foreground = Brushes.White;
            }
        }

        private void ChangeColorComboBox(ComboBox comboBox)
        {
            if (CurrentLanguageAndTheme.themeId == 0)
            {
                comboBox.Background = Brushes.White; comboBox.Foreground = Brushes.Black;
                foreach (ComboBoxItem item in comboBox.Items)
                {
                    item.Background = Brushes.White; item.Foreground = Brushes.Black;
                }
            }
            else
            {
                comboBox.Background = Brushes.Black; comboBox.Foreground = Brushes.White;
                foreach (ComboBoxItem item in comboBox.Items)
                {
                    item.Background = Brushes.Black; item.Foreground = Brushes.White;
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HideMenu();
        }
    }
}
