using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class CurrentActiveTour
    {
        public CurrentActiveTour()
        {
            InitializeComponent();
            DataContext = new CurrentActiveTourViewModel();
        }

    }
}
