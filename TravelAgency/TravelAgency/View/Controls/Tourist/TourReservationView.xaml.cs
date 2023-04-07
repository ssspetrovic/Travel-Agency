using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
     
        private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var touristView = new TouristView();
            touristView.Show();
            Close();
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

        private void MyToursButton_OnClick(object sender, RoutedEventArgs e)
        {
            var myToursView = new MyToursView();
            myToursView.Show();
            Close();
        }

        private void ToursListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((TourReservationViewModel)DataContext).IsTourSelected = ToursListView.SelectedItem != null;
        }

        private void MyVouchersButton_OnClick(object sender, RoutedEventArgs e)
        {
            var myVouchersView = new TourVouchers();
            myVouchersView.Show();
            Close();
        }
    }
}
