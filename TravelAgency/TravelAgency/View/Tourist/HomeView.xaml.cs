using System.Windows.Navigation;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomeView
    {
        public HomeView(NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new HomeViewModel(navigationService);
        }
    }
}
