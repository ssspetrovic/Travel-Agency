using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for ComplexTourRequestOverviewView.xaml
    /// </summary>
    public partial class ComplexTourRequestOverviewView
    {
        public ComplexTourRequestOverviewView(int requestId)
        {
            InitializeComponent();
            DataContext = new ComplexTourRequestOverviewViewModel(requestId);
        }
    }
}
