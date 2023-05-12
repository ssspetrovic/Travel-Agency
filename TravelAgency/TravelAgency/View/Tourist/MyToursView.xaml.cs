using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for MyToursView.xaml
    /// </summary>
    public partial class MyToursView
    {
        public MyToursView(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            InitializeComponent();
            DataContext = new MyToursViewModel(navigationService, touristViewModel);
        }
    }
}