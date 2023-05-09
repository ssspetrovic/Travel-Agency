using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guide
{
    public partial class ComplexTour
    {
        public ComplexTour()
        {
            InitializeComponent();
            DataContext = new ComplexTourViewModel();
        }

    }
}
