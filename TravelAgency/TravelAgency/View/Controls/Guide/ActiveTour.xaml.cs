using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.Repository;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for ActiveTour.xaml
    /// </summary>
    public partial class ActiveTour
    {
        public ActiveTour()
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

        private void MonitorTour_OnClick(object sender, RoutedEventArgs e)
        {
            var monitorTour = new MonitorTour();
            monitorTour.Show();
            Close();
        }

        private void ChangeCurrentKeyPoint_OnClick(object sender, RoutedEventArgs e)
        {
            var keyPoint = (string)ListViewKeyPoints.SelectedItem;
            var activeTourRepository = new ActiveTourRepository();
            activeTourRepository.UpdateKeyPoint(keyPoint);
            var allKeyPoints = activeTourRepository.GetActiveTourData("KeyPointsList").Split(", ");
            var flag = allKeyPoints.Count(location => location.Contains("False"));

            var activeTour = new ActiveTour();
            activeTour.Show();
            Close();

            if (flag == 0)
                FinishTour_OnClick(sender, e);
        }

        private void ChangeCurrentKeyPoint_OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            var keyPoint = (string) ListViewKeyPoints.SelectedItem;
            var activeTourRepository = new ActiveTourRepository();
            activeTourRepository.UpdateKeyPoint(keyPoint);
            var allKeyPoints = activeTourRepository.GetActiveTourData("KeyPointsList").Split(", ");
            var flag = allKeyPoints.Count(location => location.Contains("False"));

            var activeTour = new ActiveTour();
            activeTour.Show();
            Close();

            if (flag == 0)
                FinishTour_OnClick(sender, e);
        }

        private void TouristCheckup_OnClick(object sender, RoutedEventArgs e)
        {
            var tourist = (string)ListViewTourists.SelectedItem;
            var touristRepository = new TouristRepository();
            touristRepository.CheckTourist(tourist);

            var activeTour = new ActiveTour();
            activeTour.Show();
            Close();
        }

        private void TouristCheckup_OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var tourist = (string)ListViewTourists.SelectedItem;
            var touristRepository = new TouristRepository();
            touristRepository.CheckTourist(tourist);

            var activeTour = new ActiveTour();
            activeTour.Show();
            Close();
        }

        private void FinishTour_OnClick(object sender, RoutedEventArgs e)
        {
            var activeTourRepository = new ActiveTourRepository();
            var tourRepository = new TourRepository();
            var touristRepository = new TouristRepository();
            var keyPoints = activeTourRepository.GetActiveTourData("KeyPointsList");
            var passedKeyPoints = keyPoints.Split(", ");
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
                    var activeTour = new ActiveTour();
                    activeTour.Show();
                    Close();
                    break;
                }
            }

            if (counter != 0) return;
            MessageBox.Show("Tour has been finished!");
            var tourists = activeTourRepository.GetActiveTourData("Tourists").Split(", ");
            activeTourRepository.Remove();
            var firstTourist = touristRepository.GetByUsername(tourists[0]);

            var tour = tourRepository.GetById(firstTourist.Tour.Id);
            var tourDates = tour.Date.Split(", ").ToList();

            if (tourDates.Count < 2)
            {
                foreach (var tourist in tourists)
                {
                    var currentTourist = touristRepository.GetByUsername(tourist);
                    touristRepository.RemoveTour(currentTourist.Id);
                }

                tourRepository.Remove(firstTourist.Tour.Id);
            }
            else
            {
                var dateToday = "";
                foreach (var date in tourDates)
                {
                    if (DateTime.Compare(DateTime.Today, DateTime.Parse(date)) == 0)
                        dateToday = date;
                }

                tourRepository.RemoveDate(dateToday, tourDates, firstTourist.Tour.Id);
            }

            var monitorTour = new MonitorTour();
            monitorTour.Show();
            Close();
        }
    }
}
