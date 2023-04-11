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
using TravelAgency.DTO;

namespace TravelAgency.View.Controls.Guest1
{
    /// <summary>
    /// Interaction logic for SingleReservationView.xaml
    /// </summary>
    public partial class SingleReservationView : Window
    {
        private readonly SingleReservationViewModel _viewModel = new();

        public SingleReservationView(ReservationDTO reservation)
        {
            _viewModel.Reservation = reservation;
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var GuestView = new Guest1View();
            GuestView.Show();
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

        private void RequestListButton_OnClick(object sender, RoutedEventArgs e)
        {
            var requestListView = new RequestListView();
            requestListView.Show();
            Close();
        }

        //todo:
        private void Rate_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SubmitRating();
        }

        private void Delay_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SendDelayRequest();
            MessageBox.Show("The delay request has been sent!");
        }
    }
}
