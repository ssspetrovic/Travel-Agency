using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for RequestTourView.xaml
    /// </summary>
    public partial class RequestTourView
    {
        public RequestTourView(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            InitializeComponent();
            DataContext = new RequestTourViewModel(navigationService, touristViewModel);
        }
    }
}
