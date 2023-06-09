using System.Windows.Input;
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
            DataContext = new RequestStatsViewModel();
        }

        private void ChangeViews_KeyDown(object sender, KeyEventArgs e)
        {
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
        

        private void GetLocationStats_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            CurrentRequestStatsViewModel.DataType = "Location:" + sender.ToString()!.Split(":")[1];
            var viewModel = DataContext as RequestStatsViewModel;
            viewModel!.Selected();

        }

        private void GetLanguageStats_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            CurrentRequestStatsViewModel.DataType = "Language:" + sender.ToString()!.Split(":")[1];
            var viewModel = DataContext as RequestStatsViewModel;
            viewModel!.Selected();
        }
    }
}
