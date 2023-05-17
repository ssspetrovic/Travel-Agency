using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class CancelTour
    {
        public CancelTour()
        {
            InitializeComponent();
            DataContext = new CancelTourViewModel();
        }
    }
}
