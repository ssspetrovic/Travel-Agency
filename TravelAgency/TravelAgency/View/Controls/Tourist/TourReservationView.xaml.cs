using System.Windows;
using System.Windows.Controls;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for TourReservationView.xaml
    /// </summary>
    public partial class TourReservationView : Page
    {
        public TourReservationView()
        {
            InitializeComponent();
            DataContext = new TourReservationViewModel();
        }

        private void MakeReservationButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((TourReservationViewModel)DataContext).TourReservationService.MakeReservation();
        }

        private void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((TourReservationViewModel)DataContext).ApplyFilter();
        }

        private void ResetFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((TourReservationViewModel)DataContext).ResetFilter();
        }

        private void ToursListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((TourReservationViewModel)DataContext).IsTourSelected = ToursListView.SelectedItem != null;
        }
    }
}
