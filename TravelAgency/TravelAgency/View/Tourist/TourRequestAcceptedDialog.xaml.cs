using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
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
