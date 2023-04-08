using System.Windows;
using System.Windows.Controls.Primitives;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for TourVouchers.xaml
    /// </summary>
    public partial class TourVouchers
    {
        public TourVouchers()
        {
            InitializeComponent();
            DataContext = new TourVouchersViewModel();
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

        private void TourReservationButton_OnClick(object sender, RoutedEventArgs e)
        {
            var tourReservation = new TourReservationView();
            tourReservation.Show();
            Close();
        }

        private void MyToursButton_OnClick(object sender, RoutedEventArgs e)
        {
            var myToursView = new MyToursView();
            myToursView.Show();
            Close();
        }

        private void MyVouchersButton_OnClick(object sender, RoutedEventArgs e)
        {
            var myVouchersView = new TourVouchers();
            myVouchersView.Show();
            Close();
        }
    }
}
