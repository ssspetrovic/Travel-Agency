using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for RegularTourRequestView.xaml
    /// </summary>
    public partial class RegularTourRequestView
    {
        public RegularTourRequestView(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            InitializeComponent();
            DataContext = new RegularTourRequestViewModel(navigationService, touristViewModel);
        }
    }
}
