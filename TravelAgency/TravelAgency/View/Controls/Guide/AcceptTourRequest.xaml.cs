using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.Service;
using TravelAgency.ViewModel;
using Brushes = System.Windows.Media.Brushes;


namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for AcceptTourRequest.xaml
    /// </summary>
    public partial class AcceptTourRequest
    {
        private readonly AcceptTourViewModel _acceptTourViewModel;
        private readonly TourService _tourService;

        public AcceptTourRequest()
        {
            InitializeComponent();
            _acceptTourViewModel = new AcceptTourViewModel();
            _tourService = new TourService();
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

            if (e.Key == Key.Tab && TourRequestDataGrid.Items.Count > 0)
            {
                e.Handled = true;
                TourRequestDataGrid.SelectedIndex =
                    (TourRequestDataGrid.SelectedIndex + 1) % TourRequestDataGrid.Items.Count;
            }

            if (e.Key == Key.F)
            {
                e.Handled = true;
                FilterRequests_OnClick(sender, e);
            }

            if (e.Key == Key.Enter && TourRequestDataGrid.SelectedItem != null)
                CreateRequestedTour((DataRowView)TourRequestDataGrid.SelectedItem);
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private bool AuthenticateFilters()
        {
            if (_acceptTourViewModel.UpdateView != "Empty") return true;

            _acceptTourViewModel.UpdateView = "";
            _acceptTourViewModel.TourRequestData = _acceptTourViewModel.GetTourRequestData();
            return false;
        }

        private void FilterRequests_OnClick(object sender, RoutedEventArgs e)
        {
            var filterRequests = new FilterRequests();
            filterRequests.Closed += (_, _) =>
            {
                _acceptTourViewModel.UpdateView = filterRequests.UpdateView();
                if (AuthenticateFilters())
                    _acceptTourViewModel.TourRequestData = _acceptTourViewModel.GetTourRequestData();
                TourRequestDataGrid.ItemsSource = _acceptTourViewModel.TourRequestData;
                Visibility = Visibility.Visible;
            };
            Visibility = Visibility.Collapsed;
            filterRequests.Show();
        }

        private void CreateRequestedTour(DataRowView drv)
        {
            var selectRequestedTourDateViewModel = new SelectRequestedTourDateViewModel(drv["DateRange"].ToString()!);
            var selectRequestedTourDate = new SelectRequestedTourDate(selectRequestedTourDateViewModel);
            selectRequestedTourDate.ShowDialog();
            if (!selectRequestedTourDate.GetConformation()) return;

            if (!_tourService.CheckIfDatesExist(selectRequestedTourDate.GetSelectedDates()))
            {
                var createTour = new CreateTour
                {
                    ComboBoxLocation =
                    {
                        Text = drv["Location_Id"].ToString()!.Split(", ")[0],
                        Focusable = false, 
                        Background = Brushes.Gray
                    },
                    ComboBoxLanguage =
                    {
                        Text = drv["Language"].ToString(),
                        Focusable = false,
                        Background = Brushes.Gray
                    },
                    MaxGuestsText =
                    {
                        Text = drv["NumberOfGuests"].ToString(),
                        Focusable = false,
                        Background = Brushes.Gray
                    },
                    DateList =
                    {
                        Text = selectRequestedTourDate.GetSelectedDates(),
                        Focusable = false
                    },
                    DatePick =
                    {
                        SelectedDate = null,
                        Focusable = false,
                        Background = Brushes.Gray
                    }
                };
                createTour.Show();
                Close();
            }
            else
                MessageBox.Show("You already have a tour in the selected date range!");

        }
    }
}
