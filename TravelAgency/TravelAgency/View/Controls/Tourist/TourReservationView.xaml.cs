using System.Windows;
using System.Windows.Controls;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for TourReservationView.xaml
    /// </summary>
    public partial class TourReservationView
    {
        public TourReservationView()
        {
            InitializeComponent();
            DataContext = new TourReservationViewModel();
        }

        private void ToursListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((TourReservationViewModel)DataContext).IsTourSelected = ToursListView.SelectedItem != null;
        }
    }
}
