using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.Service;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for ReviewTour.xaml
    /// </summary>
    public partial class ReviewTour
    {
        private readonly TourRatingService _tourRatingService;

        public ReviewTour()
        {
            InitializeComponent();
            _tourRatingService = new TourRatingService();
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

            if (e.Key == Key.Tab && ListViewComments.Items.Count > 0)
            {
                e.Handled = true;
                ListViewComments.SelectedIndex =
                    (ListViewComments.SelectedIndex + 1) % ListViewComments.Items.Count;
            }

            if (e.Key == Key.Enter && (ListViewComments.SelectedItem != null || ReportButton.IsFocused))
            {
                if (ReportButton.IsFocused)
                {
                    ReportComment_OnClick(sender, e);
                }
                else
                {
                    var selectedIndex = ListViewComments.SelectedIndex;
                    ReportedCommentTxt.Text = ListViewComments.SelectedItem!.ToString()!;
                    ReportedTouristTxt.Text = ListViewTourists.Items[selectedIndex].ToString()!;
                    ReportButton.Focus();
                }
            }
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private void CreateTourCopy_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ReportComment_OnClick(object sender, RoutedEventArgs e)
        {
            if (ReportedCommentTxt.Text.Length == 0)
                MessageBox.Show("You haven't a comment to report!");
            else
            {
                _tourRatingService.ReportValidation(ReportedCommentTxt.Text, ReportedTouristTxt.Text);
                new ReviewTour().Show();
                Close();
            }

        }

    }
}
