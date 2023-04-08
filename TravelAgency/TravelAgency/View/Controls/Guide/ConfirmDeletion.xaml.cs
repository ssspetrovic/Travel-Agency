using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.View.Controls.Guide
{
    /// <summary>
    /// Interaction logic for ConfirmDeletion.xaml
    /// </summary>
    public partial class ConfirmDeletion
    {
        public ConfirmDeletion()
        {
            InitializeComponent();
        }

        private bool AuthenticateDeletion()
        {
            var userRepository = new UserRepository();
            return PasswordBox.Password == userRepository.GetByUsername(CurrentUser.Username).Password;
        }

        private void CancelTour()
        {
            var tourService = new TourService();
            var tourVoucherRepository = new TourVoucherRepository();
            var touristRepository = new TouristRepository();
            var deletedTour = tourService.GetByName(TourNameText.Text);
            var tourists = touristRepository.GetByTour(deletedTour);
            var tourDates = deletedTour.Date.Split(", ").ToList();

            foreach (var tourist in tourists)
                tourVoucherRepository.Add(new TourVoucher(tourist.Id, "Valid Voucher", DateTime.ParseExact(DateTime.Now.AddYears(1).ToString("d/M/yyyy"), "d/M/yyyy", CultureInfo.InvariantCulture)));

            if (tourDates.Count < 2)
            {
                foreach (var tourist in tourists)
                    touristRepository.RemoveTour(tourist.Id);
                tourService.Remove(deletedTour.Id);
            }

            else
            {
                var cancelledDate = CancelledTour.Date!;
                tourService.RemoveDate(cancelledDate, tourDates, deletedTour.Id);
            }
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!AuthenticateDeletion() || CancelledTour.Date == null)
            {
                MessageBox.Show("Tour cancel was not successful");
            }
            else
            {
                CancelTour();
                CancelledTour.Date = null;
            }
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DateChecked(object sender, RoutedEventArgs e)
        {
            CancelledTour.Date = ((RadioButton)sender).Content.ToString();
        }
    }
}
