using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;


namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for AcceptTourRequest.xaml
    /// </summary>
    public partial class AcceptTourRequest
    {
        public AcceptTourRequest()
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
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }
    }
}
