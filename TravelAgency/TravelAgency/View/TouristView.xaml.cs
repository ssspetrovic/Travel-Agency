using System.Windows;
using System.Windows.Controls.Primitives;
using TravelAgency.View.Controls.Tourist;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for TouristView.xaml
    /// </summary>
    public partial class TouristView : Window
    {
        public TouristView()
        {
            InitializeComponent();
        }

        private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
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
    }
}