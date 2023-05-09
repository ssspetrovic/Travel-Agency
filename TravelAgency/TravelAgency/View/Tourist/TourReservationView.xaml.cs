using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for TourReservationView.xaml
    /// </summary>
    public partial class TourReservationView
    {
        public TourReservationView(NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new TourReservationViewModel(navigationService);
        }
    }
}
