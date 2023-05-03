using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;


namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for MonitorTour.xaml
    /// </summary>
    
    public partial class MonitorTour
    {
        private readonly ActiveTourService _activeTourService;
        private readonly TouristService _touristService;
        private readonly TourService _tourService;

        public MonitorTour()
        {
            InitializeComponent();
            _activeTourService = new ActiveTourService();
            _touristService = new TouristService();
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

            if (e.Key == Key.Tab && MonitorDataGrid.Items.Count > 0)
            {
                e.Handled = true;
                MonitorDataGrid.SelectedIndex =
                    (MonitorDataGrid.SelectedIndex + 1) % MonitorDataGrid.Items.Count;
            }

            if (e.Key == Key.P && MonitorDataGrid.SelectedItem != null)
            {
                if (e.Handled) return;
                e.Handled = true;

                var selectedItem = (DataRowView)MonitorDataGrid.SelectedItem;
                var images = _tourService.GetByName(selectedItem["Name"].ToString()!).Photos;
                var links = images.Split(", ");
                foreach (var link in links)
                {
                    if (Uri.TryCreate(link, UriKind.Absolute, out var uriResult) &&
                        (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = uriResult.AbsoluteUri,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show($"Invalid link: {link}");
                    }
                }
            }

            if (e.Key == Key.Enter && MonitorDataGrid.SelectedItem != null)
                TourIsActive_OnClick(false);

            if (e.Key == Key.A)
                CurrentActiveTour_OnClick(sender, e);
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private void TourIsActive_OnClick(bool needsUpdate)
        {

            if (!_activeTourService.IsActive() || needsUpdate)
            {
                Tour selectedTour;
                if (_activeTourService.IsActive())
                    selectedTour = _tourService.GetByName(_activeTourService.GetActiveTourColumn("Name"));
                else
                {
                    var drv = (DataRowView)MonitorDataGrid.SelectedItem;
                    selectedTour = _tourService.GetByName(drv["Name"].ToString());
                }

                var tourists = _touristService.GetByTour(selectedTour);

                var activeKeyPoints = selectedTour.KeyPoints.ToDictionary(location => location!.Id, _ => false);

                var currentKeyPoint = selectedTour.Location;
                _activeTourService.Remove();
                _activeTourService.Add(new ActiveTour(selectedTour.Name, activeKeyPoints, tourists, currentKeyPoint.City));

                var myTourDtoService = new MyTourDtoService();
                myTourDtoService.UpdateStatus(selectedTour.Name, MyTourDto.TourStatus.Active);
                myTourDtoService.UpdateKeyPoint(selectedTour.Name, currentKeyPoint.ToString());
            }

            else
                MessageBox.Show("You already have an active tour!");
            
            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();
        }

        private void CurrentActiveTour_OnClick(object sender, RoutedEventArgs e)
        {

            if (_activeTourService.IsActive())
            {
                TourIsActive_OnClick(true);
            }
            else
                MessageBox.Show("There is no active tour at the moment!");
        }

    }
}
