using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.Model;
using TravelAgency.Repository;


namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for MonitorTour.xaml
    /// </summary>
    
    public partial class MonitorTour 
    {
        public MonitorTour()
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
        private void TourReview_OnClick(object sender, RoutedEventArgs e)
        {
            var reviewTour = new ReviewTour();
            reviewTour.Show();
            Close();
        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void TourIsActive_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var activeTourRepository = new ActiveTourRepository();

            if (!activeTourRepository.IsActive())
            {
                var drv = (DataRowView)MonitorDataGrid.SelectedItem;

                var tourRepository = new TourRepository();
                var selectedTour = tourRepository.GetByName(drv["Name"].ToString());
                MessageBox.Show("You selected Tour: " + selectedTour.Name);

                var touristRepository = new TouristRepository();
                var tourists = touristRepository.GetByTour(selectedTour);


                var activeKeyPoints = selectedTour.KeyPoints.ToDictionary(location => location!.Id, _ => false);

                activeTourRepository.Add(new ActiveTour(selectedTour.Name, activeKeyPoints, tourists));
            }

            else
                MessageBox.Show("You already have an active tour!");
            
            var currentTour = new CurrentTour();
            currentTour.Show();
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

            if (e.Key == Key.F6)
            {
                var reviewTour = new ReviewTour();
                reviewTour.Show();
                Close();
            }

            if (e.Key == Key.A)
            {
                CurrentActiveTour_OnClick(sender, e);
            }

            if (e.Key == Key.Oem3)
            {
                var shortcuts = new Shortcuts();
                shortcuts.Show();
                Close();
            }
        }

        private void ShortcutView_OnClick(object sender, MouseButtonEventArgs e)
        {
            var shortcuts = new Shortcuts();
            shortcuts.Show();
            Close();
        }

        private void CurrentActiveTour_OnClick(object sender, RoutedEventArgs e)
        {
            var activeTourRepository = new ActiveTourRepository();

            if (activeTourRepository.IsActive())
            {
                var currentTour = new CurrentTour();
                currentTour.Show();
                Close();
            }
            else
                MessageBox.Show("There is no active tour at the moment!");
        }
    }
}
