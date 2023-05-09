using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class TourStats
    {
        public TourStats()
        {
            InitializeComponent();
            DataContext = new TourStatsViewModel();
        }
    }
}
