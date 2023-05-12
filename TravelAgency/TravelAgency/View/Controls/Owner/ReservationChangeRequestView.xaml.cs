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
            ChangeColorListView();
            lblRequests.Content = lblRequests.Content + " (" + getReservationChangeRequestCount().ToString() + ")";
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if(lblReservationId.Content != null)
            {
                int reservationId = Convert.ToInt32(lblReservationId.Content);
                DateTime newStartDate = Convert.ToDateTime(lblNewStartDate.Content);
                DateTime newEndDate = Convert.ToDateTime(lblNewEndDate.Content);
                reservationRepository.AcceptReservationChangeRequest(reservationId, newStartDate, newEndDate);
                delayRequestRepository.AcceptDelayRequest(reservationId);
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("Reservation change request accepted successfully!", "Message");
                else
                    MessageBox.Show("Zahtev za izmenu rezervacije uspeštno prihvaćen!", "Poruka");
                Refresh();
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("You need to select a request first...", "Message");
                else
                    MessageBox.Show("Morate prvo zahtev izabrati...", "Poruka");
            }
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            if (lblReservationId.Content != null)
            {
                int reservationId = Convert.ToInt32(lblReservationId.Content);
                delayRequestRepository.RejectDelayRequest(reservationId, txtRejection.Text);
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("Reservation change request rejected successfully!", "Message");
                else
                    MessageBox.Show("Zahtev za izmenu rezervacije uspeštno odbijen!", "Poruka");
                Refresh();
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("You need to select a request first...", "Message");
                else
                    MessageBox.Show("Morate prvo zahtev izabrati...", "Poruka");
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
                if (CurrentLanguageAndTheme.languageId == 0)
                    lblAvailable.Content = "Available!";
                else
                    lblAvailable.Content = "Slobodno!";
                lblAvailable.Foreground = Brushes.LightGreen;
            }
            else
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    lblAvailable.Content = "Not available!";
                else
                    lblAvailable.Content = "Nije slobodno!";
                lblAvailable.Foreground = Brushes.DarkRed;
            }
        }

        private void ChangeColorListView()
        {
            if(CurrentLanguageAndTheme.themeId == 0)
            {
                RequestListView.Background = Brushes.White;
                RequestListView.Foreground = Brushes.Black;
            }
            else
            {
                RequestListView.Background = Brushes.Black;
                RequestListView.Foreground = Brushes.White;
            }
        }

        private int getReservationChangeRequestCount()
        {
            DelayRequestRepository delayRequestRepository = new DelayRequestRepository();
            ObservableCollection<DelayRequest> delayRequests = delayRequestRepository.GetDelayRequests(CurrentUser.Id);
            int count = delayRequests.Count;
            return count;
        }

        private void Refresh()
        {
            OwnerMainView mainWindow = null;
            foreach (Window window in Application.Current.Windows)
            {
                if (window is OwnerMainView)
                {
                    mainWindow = (OwnerMainView)window;
                    break;
                }
            }

            Frame mainFrame = mainWindow.mainFrame;
            ReservationChangeRequestView reservationChangeRequestView = new ReservationChangeRequestView();
            mainFrame.Navigate(reservationChangeRequestView);
        }
    }
}
