using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for CurrentTour.xaml
    /// </summary>
    public partial class CurrentActiveTour
    {
        private readonly ActiveTourService _activeTourService;
        private readonly FinishedTourService _finishedTourService;
        private readonly TouristService _touristService;
        private readonly TourService _tourService;
        private bool _currentListView;


        public CurrentActiveTour()
        {
            InitializeComponent();
            _activeTourService = new ActiveTourService();
            _finishedTourService = new FinishedTourService();
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

        private void Home_OnClick(object sender, RoutedEventArgs e)
        {
            var guideView = new GuideView();
            guideView.Show();
            Close();
        }

        private void MonitorTour_OnClick(object sender, RoutedEventArgs e)
        {
            var monitorTour = new MonitorTour();
            monitorTour.Show();
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

            if (e.Key == Key.F2 || e.Key == Key.Escape)
            {
                var monitorTour = new MonitorTour();
                monitorTour.Show();
                Close();
            }

            if (e.Key == Key.Oem3)
            {
                var shortcuts = new Shortcuts();
                shortcuts.Closed += Shortcuts_Closed;
                Visibility = Visibility.Collapsed;
                shortcuts.Show();
            }

            if (e.Key == Key.E)
                FinishTour_OnClick(sender, e);

            if (e.Key == Key.C)
                CheckAllGuests_OnClick(sender, e);


            if (e.Key == Key.LeftShift)
            {
                _currentListView = !_currentListView;
                e.Handled = true;

                if (_currentListView)
                    ListViewTourists.SelectedIndex = -1;
                else
                    ListViewKeyPoints.SelectedIndex = -1;

            }

            if (e.Key == Key.Tab)
            {
                if (ListViewKeyPoints.Items.Count > 0 && !_currentListView)
                {
                    e.Handled = true;
                    var index = (ListViewKeyPoints.SelectedIndex + 1) % ListViewKeyPoints.Items.Count;
                    if (ListViewKeyPoints.ItemContainerGenerator.ContainerFromIndex(index) is ListViewItem listViewItem)
                    {
                        listViewItem.IsSelected = true;
                        listViewItem.Focus();
                    }
                }
                else if (ListViewTourists.Items.Count > 0 && _currentListView)
                {
                    e.Handled = true;
                    var index = (ListViewTourists.SelectedIndex + 1) % ListViewTourists.Items.Count;
                    if (ListViewTourists.ItemContainerGenerator.ContainerFromIndex(index) is ListViewItem listViewItem)
                    {
                        listViewItem.IsSelected = true;
                        listViewItem.Focus();
                    }
                }
            }


        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private void ChangeCurrentKeyPoint_OnClick(object sender, RoutedEventArgs e)
        {
            var keyPoint = (string)ListViewKeyPoints.SelectedItem;
            _activeTourService.UpdateKeyPoint(keyPoint);
            var allKeyPoints = _activeTourService.GetActiveTourColumn("KeyPointsList").Split(", ");
            var flag = allKeyPoints.Count(location => location.Contains("False"));

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();

            if (flag == 0)
                FinishTour_OnClick(sender, e);
        }

        private void ChangeCurrentKeyPoint_OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            var keyPoint = (string) ListViewKeyPoints.SelectedItem;
            _activeTourService.UpdateKeyPoint(keyPoint);
            var allKeyPoints = _activeTourService.GetActiveTourColumn("KeyPointsList").Split(", ");
            var flag = allKeyPoints.Count(location => location.Contains("False"));

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();

            if (flag == 0)
                FinishTour_OnClick(sender, e);
        }

        private void TouristCheckup_OnClick(object sender, RoutedEventArgs e)
        {
            var tourist = (string)ListViewTourists.SelectedItem;
            _touristService.CheckTouristAppearance(tourist);

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();
        }

        private void TouristCheckup_OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var tourist = (string)ListViewTourists.SelectedItem;
            _touristService.CheckTouristAppearance(tourist);

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();
        }

        private void CheckPassedKeyPoints(IEnumerable<string> passedKeyPoints)
        {
            var counter = 0;

            foreach (var location in passedKeyPoints)
            {
                if (location.Contains("False"))
                {
                    var question = MessageBox.Show("We haven't passed all key points, are you sure you want to end the tour?", "WAIT", MessageBoxButton.YesNo);
                    if (question == MessageBoxResult.Yes)
                    {
                        counter = 0;
                        break;
                    }

                    counter = 1;
                    var currentTour = new CurrentActiveTour();
                    currentTour.Show();
                    Close();
                    break;
                }
            }

            if (counter != 0) return;
            MessageBox.Show("Tour has been finished!");
        }

        public void RemoveTour(List<string> tourDates, IReadOnlyList<string> tourists)
        {
            
            var firstTourist = _touristService.GetByUsername(tourists[0]);

            if (tourDates.Count < 2)
            {
                foreach (var tourist in tourists)
                {
                    var currentTourist = _touristService.GetByUsername(tourist);
                    _touristService.RemoveTour(currentTourist.Id);
                }
                _tourService.Remove(firstTourist.Tour.Id);
            }
            else
            {
                var dateToday = "";
                foreach (var date in tourDates)
                {
                    if (date.Equals(DateTime.Today.ToString("MM/dd/yyyy", new CultureInfo("en-US"))))
                        dateToday = date;
                }

                foreach (var tourist in tourists)
                {
                    var currentTourist = _touristService.GetByUsername(tourist);
                    _touristService.RemoveTour(currentTourist.Id);
                    if(TouristAppearance.Present != currentTourist.TouristAppearance)
                        _touristService.UpdateAppearance(currentTourist.Id, TouristAppearance.Absent);
                }

                _tourService.RemoveDate(dateToday, tourDates, firstTourist.Tour.Id);
            }
        }

        private void AddFinishedTour()
        {
            var tourists = _activeTourService.GetActiveTourColumn("Tourists").Split(", ");
            var firstTourist = _touristService.GetByUsername(tourists[0]);

            var keyPoints = _activeTourService.GetActiveTourColumn("KeyPointsList");
            var keyPointParts = keyPoints.Split(", ");
            CheckPassedKeyPoints(keyPointParts);

            var tour = _tourService.GetById(firstTourist.Tour.Id);
            
            // Setting MyTourDto status
            var myTourDtoService = new MyTourDtoService();
            myTourDtoService.UpdateStatus(tour.Name, MyTourDto.TourStatus.Finished);

            if (_finishedTourService.CheckExistingTours(tour))
                _finishedTourService.Edit(new FinishedTour(tour.Id, tour.Name, _tourService.GetKeyPoints(string.Join(", ", keyPointParts.Select(p => p[..p.IndexOf(':')]))), _touristService.GetByTour(tour), tour.Date.Split(", ")[0]));
            else
                _finishedTourService.Add(new FinishedTour(tour.Id, tour.Name, _tourService.GetKeyPoints(string.Join(", ", keyPointParts.Select(p => p[..p.IndexOf(':')]))), _touristService.GetByTour(tour), tour.Date.Split(", ")[0]));

            RemoveTour(tour.Date.Split(", ").ToList(), tourists);
            _activeTourService.Remove();
        }

        private void FinishTour_OnClick(object sender, RoutedEventArgs e)
        {
            AddFinishedTour();
            var reviewTour = new ReviewTour();
            reviewTour.Show();
            Close();
        }

        private void CheckAllGuests_OnClick(object sender, RoutedEventArgs e)
        {
            var tourists = _activeTourService.GetActiveTourColumn("Tourists");
            var counter = 0;

            foreach (var tourist in tourists.Split(", "))
            {
                if (_touristService.GetByUsername(tourist).TouristAppearance == TouristAppearance.Unknown)
                    counter++;
            }

            if (counter == 0)
                MessageBox.Show("All tourists were already checked!");
            else
                _touristService.CheckAllTouristAppearances(tourists);

            var currentTour = new CurrentActiveTour();
            currentTour.Show();
            Close();
        }
    }
}
