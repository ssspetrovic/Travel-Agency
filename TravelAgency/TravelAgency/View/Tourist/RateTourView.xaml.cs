using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for RateTourView.xaml
    /// </summary>
    public partial class RateTourView
    {
        public RateTourView(NavigationService navigationService, TouristViewModel touristViewModel, string selectedTourName = "/")
        {
            InitializeComponent();
            DataContext = new RateTourViewModel(navigationService, touristViewModel, selectedTourName);
        }
    }
}
