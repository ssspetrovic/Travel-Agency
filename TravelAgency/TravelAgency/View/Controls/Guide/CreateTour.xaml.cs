using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour
    {
        public CreateTour()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern nint SendMessage(nint wnd, int wMsg, nint wParam, nint lParam);

        private void PanelControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void PanelControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void Button_CloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_MaximizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void Button_MinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Home_OnClick(object sender, RoutedEventArgs e)
        {
            var guideView = new GuideView();
            guideView.Show();
            Close();
        }

        private void CreateTour_OnClick(object sender, RoutedEventArgs e)
        {
            var createTour = new CreateTour();
            createTour.Show();
            Close();
        }

        private void MonitorTour_OnClick(object sender, RoutedEventArgs e)
        {
            var monitorTour = new MonitorTour();
            monitorTour.Show();
            Close();
        }

        private void ActiveTour_OnClick(object sender, RoutedEventArgs e)
        {
            var activeTourRepository = new ActiveTourRepository();
            if (activeTourRepository.IsActive())
            {
                var activeTour = new ActiveTour();
                activeTour.Show();
                Close();
            }
            else
                MessageBox.Show("There is no active tour!");
        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            var currentUserRepository = new CurrentUserRepository();
            currentUserRepository.Remove();
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private bool AuthenticateTourInfo()
        {
            if (NameText.Text.Length < 3)
            {
                ErrorMessageText.Text = "Incorrect name.";
                NameText.Focus();
                return false;
            }

            if (ComboBoxLocation.Text.Length == 0)
            {
                ErrorMessageText.Text = "Insert a location.";
                ComboBoxLocation.Focus();
                return false;
            }

            if (ComboBoxLanguage.Text.Length == 0)
            {
                ErrorMessageText.Text = "Insert a language.";
                ComboBoxLanguage.Focus();
                return false;
            }

            if (MaxGuestsText.Text.Length == 0)
            {
                ErrorMessageText.Text = "Insert a number";
                MaxGuestsText.Focus();
                return false;
            }

            if (!Regex.IsMatch(MaxGuestsText.Text, @"^[0-9]+$"))
            {
                ErrorMessageText.Text = "You can only insert numbers.";
                MaxGuestsText.Focus();
                return false;
            }

            if (KeyPointsList.Text.Length == 0)
            {
                ErrorMessageText.Text = "No key points have been added.";
                ComboBoxKeyPoints.Focus();
                return false;
            }

            if (!KeyPointsList.Text.Contains(","))
            {
                ErrorMessageText.Text = "Insert at least two key points, start and end key point!";
                ComboBoxKeyPoints.Focus();
                return false;
            }

            if (DateList.Text.Length == 0)
            {
                ErrorMessageText.Text = "Insert at least one date.";
                DatePick.Focus();
                return false;
            }

            if (DurationText.Text.Length == 0)
            {
                ErrorMessageText.Text = "Insert a number.";
                DurationText.Focus();
                return false;
            }

            if (!Regex.IsMatch(DurationText.Text, @"([0-9]*[.])?[0-9]+$"))
            {
                ErrorMessageText.Text = "You can only insert a number.";
                DurationText.Focus();
                return false;
            }

            if (ImagesList.Text.Length == 0)
            {
                ErrorMessageText.Text = "Insert an image link.";
                ImagesText.Focus();
                return false;
            }

            ErrorMessageText.Text = "";
            return true;
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            if (!AuthenticateTourInfo()) return;

            var tourRepository = new TourRepository();
            var locationRepository = new LocationRepository();
            var currentLocation =
                locationRepository.GetByCity(ComboBoxLocation.Text);
            var maxGuests = int.Parse(MaxGuestsText.Text);
            var duration = float.Parse(DurationText.Text);
            var cities = new List<string>(KeyPointsList.Text.Split(", "));
            var locationList = locationRepository.GetByAllCities(cities);
            var language = tourRepository.FindLanguage(ComboBoxLanguage.Text);

            if (currentLocation != null)
            {
                tourRepository.Add(new TourModel(NameText.Text, currentLocation,
                    DescriptionText.Text, language, maxGuests, locationList!, DateList.Text,
                    duration, ImagesList.Text));
                MessageBox.Show("Tour Added Successfully.");
            }

            else
                MessageBox.Show("UNSUCCESSFUL TOUR");
            var guideView = new GuideView();
            guideView.Show();
            Close();
            
        }

        private void AddNewDate_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var date = Convert.ToDateTime(DatePick.Text).ToString("MM/dd/yyyy");
            var hasText = false;
            if (DateList.Text.Contains(DatePick.Text))
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                DateList.Text.Replace(DatePick.Text, "");
                hasText = true;
            }

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            if (DateList.Text.Contains(",, "))
                DateList.Text.Replace(",, ", ", ");

            if(!hasText)
                if(DateList.Text.Length == 0)
                    DateList.Text += date;
                else
                    DateList.Text += ", " + date;

            if (DateList.Text.StartsWith(", "))
                DateList.Text = DateList.Text.Substring(2, DateList.Text.Length - 2);
                
            DatePick.Text = "";
        }

        private void DeleteDates_OnClick(object sender, RoutedEventArgs e)
        {
            DateList.Text = "";
        }

        private void AddKeyPoints_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var hasText = false;
            if (KeyPointsList.Text.Contains(ComboBoxKeyPoints.Text))
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                KeyPointsList.Text.Replace(ComboBoxKeyPoints.Text, "");
                hasText = true;
            }

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            if (KeyPointsList.Text.Contains(",, "))
                ComboBoxKeyPoints.Text.Replace(",, ", ", ");

            if (!hasText)
                if (KeyPointsList.Text.Length == 0)
                    KeyPointsList.Text += ComboBoxKeyPoints.Text;
                else
                    KeyPointsList.Text += ", " + ComboBoxKeyPoints.Text;

            if (KeyPointsList.Text.StartsWith(", "))
                 KeyPointsList.Text =  KeyPointsList.Text.Substring(2, KeyPointsList.Text.Length - 2);

            ComboBoxKeyPoints.Text = "";
        }

        private void DeleteKeyPoints_OnClick(object sender, RoutedEventArgs e)
        {
            KeyPointsList.Text = "";
        }

        private void AddImages_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var hasText = false;
            if (ImagesList.Text.Contains(ImagesText.Text))
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                ImagesList.Text.Replace(ImagesText.Text, "");
                hasText = true;
            }

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            if (ImagesList.Text.Contains(",, "))
                ImagesList.Text.Replace(",, ", ", ");

            if(!hasText)
                if (ImagesList.Text.Length == 0)
                    ImagesList.Text += ImagesText.Text;
                else
                    ImagesList.Text += ", " + ImagesText.Text;

            if (ImagesList.Text.StartsWith(", "))
                ImagesList.Text = ImagesList.Text.Substring(2, ImagesList.Text.Length - 2);

            ImagesText.Text = "";
        }

        private void DeleteImages_OnClick(object sender, RoutedEventArgs e)
        {
            ImagesList.Text = "";
        }
    }
}
