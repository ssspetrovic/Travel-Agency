using System.Windows.Navigation;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
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
