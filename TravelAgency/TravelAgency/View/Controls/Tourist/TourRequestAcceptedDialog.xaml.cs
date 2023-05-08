using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for TourRequestAcceptedDialog.xaml
    /// </summary>
    public partial class TourRequestAcceptedDialog
    {
        public TourRequestAcceptedDialog(RegularTourRequestViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
