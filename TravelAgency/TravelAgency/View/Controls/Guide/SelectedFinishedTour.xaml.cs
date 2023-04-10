using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows;
using System;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for SelectedFinishedTour.xaml
    /// </summary>
    public partial class SelectedFinishedTour
    {
        public SelectedFinishedTour()
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


        private void AllFinishedTours_OnClick(object sender, RoutedEventArgs e)
        {
            var allfinishedTours = new AllFinishedTours();
            allfinishedTours.Show();
            Close();
        }

        private void ShortcutView_OnClick(object sender, MouseButtonEventArgs e)
        {
            var shortcuts = new Shortcuts();
            shortcuts.Show();
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
                var allfinishedTours = new AllFinishedTours();
                allfinishedTours.Show();
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
