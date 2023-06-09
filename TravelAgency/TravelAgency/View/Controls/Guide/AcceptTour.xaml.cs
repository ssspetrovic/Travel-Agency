using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class AcceptTour
    {
        public AcceptTour()
        {
            InitializeComponent();
            DataContext = new AcceptTourViewModel();
        }

    }
}
