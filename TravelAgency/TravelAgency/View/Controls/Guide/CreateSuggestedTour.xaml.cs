using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class CreateSuggestedTour
    {
        public CreateSuggestedTour()
        {
            InitializeComponent();
            DataContext = new CreateSuggestedTourViewModel();
        }
    }
}
