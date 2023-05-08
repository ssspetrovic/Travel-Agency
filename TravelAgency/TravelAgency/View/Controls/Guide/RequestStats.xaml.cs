using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows;
using System;
using System.Windows.Controls;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class RequestStats
    {
        private bool _currentListView;

        public RequestStats()
        {
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

            if (e.Key == Key.LeftShift)
            {
                _currentListView = !_currentListView;
                e.Handled = true;

                if (_currentListView)
                    ListViewLanguages.SelectedIndex = -1;
                else
                    ListViewLocations.SelectedIndex = -1;

            }

            if (e.Key == Key.Tab)
            {
                if (ListViewLocations.Items.Count > 0 && !_currentListView)
                {
                    e.Handled = true;
                    var index = (ListViewLocations.SelectedIndex + 1) % ListViewLocations.Items.Count;
                    if (ListViewLocations.ItemContainerGenerator.ContainerFromIndex(index) is ListViewItem listViewItem)
                    {
                        listViewItem.IsSelected = true;
                        listViewItem.Focus();
                    }
                }
                else if (ListViewLanguages.Items.Count > 0 && _currentListView)
                {
                    e.Handled = true;
                    var index = (ListViewLanguages.SelectedIndex + 1) % ListViewLanguages.Items.Count;
                    if (ListViewLanguages.ItemContainerGenerator.ContainerFromIndex(index) is ListViewItem listViewItem)
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

        private void GetLocationStats_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            CurrentRequestStatsViewModel.DataType = "Location:" + sender.ToString()!.Split(":")[1];
            var currentRequestStatsViewModel = new CurrentRequestStatsViewModel();
            var currentRequestStats = new CurrentRequestStats(currentRequestStatsViewModel);
            currentRequestStats.Show();
            var currentWindow = Window.GetWindow(this);
            currentWindow?.Close();
        }

        private void GetLanguageStats_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            CurrentRequestStatsViewModel.DataType = "Language:" + sender.ToString()!.Split(":")[1];
            var currentRequestStatsViewModel = new CurrentRequestStatsViewModel();
            var currentRequestStats = new CurrentRequestStats(currentRequestStatsViewModel);
            currentRequestStats.Show();
            var currentWindow = Window.GetWindow(this);
            currentWindow?.Close();
        }
    }
}
