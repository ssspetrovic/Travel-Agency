using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.ViewModel;


namespace TravelAgency.View.Controls.Guide
{
    public partial class CurrentRequestStats
    {
        private readonly RequestTourService _requestTourService;
        private string _year = "";

        public CurrentRequestStats(CurrentRequestStatsViewModel currentRequestStatsViewModel)
        {
            DataContext = currentRequestStatsViewModel;
            InitializeComponent();
            _requestTourService = new RequestTourService();
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
            WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void Button_MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ShortcutView_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new View.Guide
            {
                Content = new Shortcuts(),
                Title = "Shortcuts"
            };
            window.Show();
            Close();
        }

        private void RequestStats_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new View.Guide
            {
                Content = new RequestStats(),
                Title = "Request Stats"
            };
            window.Show();
            Close();
        }

        private void ChangeViews_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var window = new View.Guide
                {
                    Content = new RequestStats(),
                    Title = "Request Stats"
                };
                window.Show();
                Close();
            }

            if (e.Key == Key.Oem3)
            {
                var shortcuts = new Shortcuts();
                shortcuts.Closed += Shortcuts_Closed;
                Visibility = Visibility.Collapsed;
                shortcuts.Show();
            }

            if (e.Key == Key.Tab)
            {
                CurrentRequestStatsTabControl.SelectedIndex = (CurrentRequestStatsTabControl.SelectedIndex + 1) % CurrentRequestStatsTabControl.Items.Count;
                var selectedTab = (CurrentRequestTabs) CurrentRequestStatsTabControl.SelectedItem;
                if (selectedTab != null)
                    _year = selectedTab.Title;
            }

            if (e.Key is >= Key.F1 and <= Key.F12 || e.SystemKey == Key.F10)
            {
                if (!int.TryParse(_year, out _)) return;
                var month = e.Key - Key.F1 + 1;
                if (e.SystemKey == Key.F10)
                    month = 10;
                var showMonthlyRequestStatsViewModel = new ShowMonthlyRequestStatsViewModel()
                {
                    NumberOfRequests = _requestTourService.GetRequestsByDate("", month, _year).Sum(tour => tour.NumberOfGuests).ToString(),
                    CurrentMonth = new DateTimeFormatInfo().GetMonthName(month) + ", " + _year
                };
                var showMonthlyRequestStats = new ShowMonthlyRequestStats(showMonthlyRequestStatsViewModel);
                showMonthlyRequestStats.Show();
            }
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }
    }
}
