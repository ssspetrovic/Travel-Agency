using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomeView
    {
        public HomeView(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            InitializeComponent();
            DataContext = new HomeViewModel(navigationService, touristViewModel);
        }
    }
}
