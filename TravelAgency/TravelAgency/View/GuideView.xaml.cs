using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using TravelAgency.Repository;
using TravelAgency.View.Controls.Guide;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for GuideView.xaml
    /// </summary>
    public partial class GuideView 
    {
        public GuideView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        [DllImport("user32.dll")]
        public static extern nint SendMessage(nint wnd,  int wMsg, nint wParam, nint lParam);

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

            if (e.Key == Key.Oem3)
            {
                var shortcuts = new Shortcuts();
                shortcuts.Show();
                Close();
            }
        }

        private void ShortcutView_OnClick(object sender, RoutedEventArgs e)
        {
            var shortcuts = new Shortcuts();
            shortcuts.Show();
            Close();
        }

        private void CancelTour_OnClick(object sender, RoutedEventArgs e)
        {
            var cancelTour = new CancelTour();
            cancelTour.Show(); 
            Close();
        }
    }
}
