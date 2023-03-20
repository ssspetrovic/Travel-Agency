using System.Windows;
using System.Windows.Controls.Primitives;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guest1
{
    /// <summary>
    /// Interaction logic for TourReservationView.xaml
    /// </summary>
    public partial class AccommodationReservationView : Window
    {
        private readonly AccommodationReservationViewModel _viewModel = new();

        public AccommodationReservationView()
        {
            InitializeComponent();
            DataContext = _viewModel;
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

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var touristView = new TouristView();
            touristView.Show();
            Close();
        }

        private void MakeReservationButton_OnClick(object sender, RoutedEventArgs e)
        {
            AccommodationReservationViewModel.MakeReservation(sender, e);
        }
    }
}
