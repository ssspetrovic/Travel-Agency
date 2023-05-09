using System.Windows;
using System.Windows.Controls;
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

        private void ToursListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((TourReservationViewModel)DataContext).IsTourSelected = ToursListView.SelectedItem != null;
        }
    }
}
