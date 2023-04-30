using System.Windows;
using System.Windows.Input;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class ShowMonthlyRequestStats
    {
        public ShowMonthlyRequestStats(ShowMonthlyRequestStatsViewModel showMonthlyRequestStatsViewModel)
        {
            InitializeComponent();
            DataContext = showMonthlyRequestStatsViewModel;
        }

        private void ShowMonthlyRequestStats_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
