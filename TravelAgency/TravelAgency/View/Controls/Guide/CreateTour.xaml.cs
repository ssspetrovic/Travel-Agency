using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.Model;
using TravelAgency.Service;
using static System.Char;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour
    {
        private readonly LocationService _locationService;
        private readonly TourService _tourService;

        public CreateTour()
        {
            InitializeComponent();
            _locationService = new LocationService();
            _tourService = new TourService();
            NameText.Focus();
            DatePick.SelectedDate = DateTime.Today;
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

        private void ShortcutView_OnClick(object sender, RoutedEventArgs e)
        {
            var shortcuts = new Shortcuts();
            shortcuts.Show();
            Close();
        }

        private void Home_OnClick(object sender, RoutedEventArgs e)
        {
            var guideView = new GuideView();
            guideView.Show();
            Close();
        }

        private void NewTour_OnClick(object sender, RoutedEventArgs e)
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

        private void CancelTour_OnClick(object sender, RoutedEventArgs e)
        {
            var cancelTour = new CancelTour();
            cancelTour.Show();
            Close();
        }

        private void TourStats_OnClick(object sender, RoutedEventArgs e)
        {
            var tourStats = new TourStats();
            tourStats.Show();
            Close();
        }

        private void TourReview_OnClick(object sender, RoutedEventArgs e)
        {
            var reviewTour = new ReviewTour();
            reviewTour.Show();
            Close();
        }

        private void AcceptTourRequest_OnClick(object sender, RoutedEventArgs e)
        {
            var acceptTourRequest = new AcceptTourRequest();
            acceptTourRequest.Show();
            Close();
        }

        private void ComplexTourRequest_OnClick(object sender, RoutedEventArgs e)
        {
            var complexTourRequest = new ComplexTourRequest();
            complexTourRequest.Show();
            Close();
        }

        private void RequestStats_OnClick(object sender, RoutedEventArgs e)
        {
            var requestStats = new RequestStats();
            requestStats.Show();
            Close();
        }

        private void CreateSuggestedTour_OnClick(object sender, RoutedEventArgs e)
        {
            var createSuggestedTour = new CreateSuggestedTour();
            createSuggestedTour.Show();
            Close();
        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void Resign_OnClick(object sender, RoutedEventArgs e)
        {
            var resign = new Resign();
            resign.Show();
            Close();
        }

        private void ChangeViews_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                var guideView = new GuideView();
                guideView.Show();
                Close();
            }

            if (e.Key == Key.F2)
            {
                var createTour = new CreateTour();
                createTour.Show();
                Close();
            }

            if (e.Key == Key.F3)
            {
                var monitorTour = new MonitorTour();
                monitorTour.Show();
                Close();
            }

            if (e.Key == Key.F4)
            {
                var cancelTour = new CancelTour();
                cancelTour.Show();
                Close();
            }

            if (e.Key == Key.F5)
            {
                var tourStats = new TourStats();
                tourStats.Show();
                Close();
            }

            if (e.Key == Key.F6)
            {
                var reviewTour = new ReviewTour();
                reviewTour.Show();
                Close();
            }

            if (e.Key == Key.F7)
            {
                var acceptTourRequest = new AcceptTourRequest();
                acceptTourRequest.Show();
                Close();
            }

            if (e.Key == Key.F8)
            {
                var complexTourRequest = new ComplexTourRequest();
                complexTourRequest.Show();
                Close();
            }

            if (e.Key == Key.F9)
            {
                var requestStats = new RequestStats();
                requestStats.Show();
                Close();
            }

            if (e.SystemKey == Key.F10)
            {
                var createSuggestedTour = new CreateSuggestedTour();
                createSuggestedTour.Show();
                Close();
            }

            if (e.Key == Key.F11)
            {
                var resign = new Resign();
                resign.Show();
                Close();
            }

            if (e.Key == Key.Oem3)
            {
                var shortcuts = new Shortcuts();
                shortcuts.Closed += Shortcuts_Closed;
                Visibility = Visibility.Collapsed;
                shortcuts.Show();
            }

            if (e.Key == Key.Tab && BtnConfirm.IsFocused)
            {
                e.Handled = true;
                NameText.Focus();
                CreateTourScrollViewer.ScrollToTop();
            }

            if (e.Key == Key.Enter && ComboBoxKeyPoints.IsFocused)
                AddKeyPoints_OnClick();

            if (e.Key == Key.Delete && ComboBoxKeyPoints.IsFocused)
                DeleteKeyPoints_OnClick();

            if (e.Key == Key.Enter && ImagesText.IsFocused)
                AddImages_OnClick();

            if (e.Key == Key.Delete && ImagesText.IsFocused)
                DeleteImages_OnClick();

            if (e.Key == Key.Enter && BtnConfirm.IsFocused)
                Confirm_OnClick(sender, e);
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private void DatePick_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                AddNewDate_OnClick();
            if (e.Key == Key.Delete)
                DeleteDates_OnClick();
            if (IsLetterOrDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)))
                DatePick.SelectedDate = null;
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
            
            var currentLocation =  _locationService.GetByCity(ComboBoxLocation.Text);
            var maxGuests = int.Parse(MaxGuestsText.Text);
            var duration = float.Parse(DurationText.Text);
            var cities = new List<string>(KeyPointsList.Text.Split(", "));
            var locationList = _locationService.GetByAllCities(cities);
            var language = _tourService.FindLanguage(ComboBoxLanguage.Text);

            if (currentLocation != null)
            {
                _tourService.Add(new Tour(NameText.Text, currentLocation,
                    DescriptionText.Text, language, maxGuests, locationList, DateList.Text,
                    duration, ImagesList.Text));
                MessageBox.Show("Tour Added Successfully.");
            }

            else
                MessageBox.Show("UNSUCCESSFUL TOUR");
            var guideView = new GuideView();
            guideView.Show();
            Close();
            
        }

        private void AddNewDate_OnClick()
        {

            var date = Convert.ToDateTime(DatePick.Text).ToString("MM/dd/yyyy",new CultureInfo("en-US"));
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
            DatePick.Focus();
        }

        private void DeleteDates_OnClick()
        {
            DateList.Text = "";
            DatePick.Focus();
        }

        private void AddKeyPoints_OnClick()
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
            ComboBoxKeyPoints.Focus();
        }

        private void DeleteKeyPoints_OnClick()
        {
            KeyPointsList.Text = "";
            ComboBoxKeyPoints.Focus();
        }

        private void AddImages_OnClick()
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
            ImagesText.Focus();
        }

        private void DeleteImages_OnClick()
        {
            ImagesList.Text = "";
            ImagesText.Focus();
        }
    }
}
