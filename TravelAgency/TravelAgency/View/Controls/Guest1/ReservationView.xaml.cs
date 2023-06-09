using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Guest1
{
    /// <summary>
    /// Interaction logic for ReservatoinView.xaml
    /// </summary>
    public partial class ReservatoinView : Window
    {
        private readonly ReservationViewModel _viewModel = new();
        public ReservatoinView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
        private void Profile_OnClick(object sender, RoutedEventArgs e)
        {
            var ProfileView = new ProfileView();
            ProfileView.Show();
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


        private void Details_OnClick(object sender, RoutedEventArgs e)
        {
            var ReservationDetail = new SingleReservationView(_viewModel.SelectedReservations);
            ReservationDetail.Show();
            Close();
        }

        private void RequestListButton_OnClick(object sender, RoutedEventArgs e)
        {
            var requestListView = new RequestListView();
            requestListView.Show();
            Close();
        }

        private void CancelReservation_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelSelectedReservation();
            var ReservationView = new ReservatoinView();
            ReservationView.Show();
            Close();
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var GuestView = new Guest1View();
            GuestView.Show();
            Close();
        }
    }
}
