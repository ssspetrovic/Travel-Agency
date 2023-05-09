using TravelAgency.ViewModel;
namespace TravelAgency.View.Controls.Guide
{
    public partial class MonitorTour
    {
        public MonitorTour()
        {
            InitializeComponent();
            DataContext = new MonitorTourViewModel();
        }
    }
}
