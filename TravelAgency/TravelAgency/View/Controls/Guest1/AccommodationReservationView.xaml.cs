using System.Windows;
using System.Windows.Controls.Primitives;
using TravelAgency.DTO;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReservationView.xaml
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
        private void Profile_OnClick(object sender, RoutedEventArgs e)
        {
            var ProfileView = new ProfileView();
            ProfileView.Show();
            Close();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var GuestView = new Guest1View();
            GuestView.Show();
            Close();
        }

        private void RequestListButton_OnClick(object sender, RoutedEventArgs e)
        {
            var requestListView = new RequestListView();
            requestListView.Show();
            Close();
        }

        private void Reservation_OnClick(object sender, RoutedEventArgs e)
        {
            var ReservationView = new ReservatoinView();
            ReservationView.Show();
            Close();
        }


        private void Accommodation_OnClick(object sender, RoutedEventArgs e)
        {
            var AccommodationReservationView = new AccommodationReservationView();
            AccommodationReservationView.Show();
            Close();
        }


        private void MakeReservationButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.MakeReservation();
        }

        private void Advanced_OnClick(object sender, RoutedEventArgs e)
        {
            var AdvancedSearchView = new AdvancedSearchView();
            AdvancedSearchView.Show();
            Close();
        }
       

        private void ToursListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
