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
    }
}
