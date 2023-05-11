using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for ReservationChangeRequestView.xaml
    /// </summary>
    public partial class ReservationChangeRequestView : Page
    {
        private readonly ReservationChangeRequestsViewModel _viewModel = new();
        ReservationRepository reservationRepository = new ReservationRepository();
        DelayRequestRepository delayRequestRepository = new DelayRequestRepository();
        public ReservationChangeRequestView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int reservationId = Convert.ToInt32(lblReservationId.Content);
                DateTime newStartDate = Convert.ToDateTime(lblNewStartDate.Content);
                DateTime newEndDate = Convert.ToDateTime(lblNewEndDate.Content);
                reservationRepository.AcceptReservationChangeRequest(reservationId, newStartDate, newEndDate);
                delayRequestRepository.AcceptDelayRequest(reservationId);
                MessageBox.Show("Request accepted successfully");
            }
            catch
            {
                MessageBox.Show("Select a request first...");
            }
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int reservationId = Convert.ToInt32(lblReservationId.Content);
                delayRequestRepository.RejectDelayRequest(reservationId, txtRejection.Text);
                MessageBox.Show("Request rejected successfully");
            }
            catch
            {
                MessageBox.Show("Select a request first...");
            }
        }

        private void RequestListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Reservation> reservationList = reservationRepository.GetAll();
            bool available = true;
            foreach (Reservation reservation in reservationList)
            {
                if (reservation.Id != Convert.ToInt32(lblReservationId.Content))
                {
                    if (reservation.AccommodationId == Convert.ToInt32(lblAccommodation.Content))
                    {
                        DateTime newStartDate = Convert.ToDateTime(lblNewStartDate.Content);
                        DateTime newEndDate = Convert.ToDateTime(lblNewEndDate.Content);
                        if (reservation.StartDate >= newStartDate && reservation.StartDate <= newEndDate)
                        {
                            available = false; break;
                        }
                        if (reservation.EndDate >= newStartDate && reservation.EndDate <= newEndDate)
                        {
                            available = false; break;
                        }
                    }
                }
            }
            if (available)
            {
                lblAvailable.Content = "Available!";
                lblAvailable.Foreground = Brushes.LightGreen;
            }
            else
            {
                lblAvailable.Content = "Not available!";
                lblAvailable.Foreground = Brushes.DarkRed;
            }
        }
    }
}
