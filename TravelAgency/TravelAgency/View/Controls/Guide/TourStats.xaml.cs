using System;
using System.Windows;
using System.Windows.Input;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class TourStats
    {
        public TourStats()
        {
            DataContext = new TourStatsViewModel();
            InitializeComponent();
        }

        private void ChangeViews_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem3)
            {
                var shortcuts = new Shortcuts();
                shortcuts.Closed += Shortcuts_Closed;
                Visibility = Visibility.Collapsed;
                shortcuts.Show();
            }

            if (e.Key == Key.Tab)
            {
                TourStatsTabControl.SelectedIndex = (TourStatsTabControl.SelectedIndex + 1) % TourStatsTabControl.Items.Count;
            }

            if (e.Key == Key.Enter)
                AllFinishedTours_OnClick(sender, e);
        }

        private void Shortcuts_Closed(object? sender, EventArgs eventArgs)
        {
            Visibility = Visibility.Visible;
        }

        private void AllFinishedTours_OnClick(object sender, RoutedEventArgs e)
        {
            var allFinishedTours = new AllFinishedTours();
            allFinishedTours.Show();
            var currentWindow = Window.GetWindow(this);
            currentWindow?.Close();
        }
    }
}
