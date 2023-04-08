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
        private readonly TouristService _touristService;
        private readonly TourService _tourService;

        public ConfirmDeletion()
        {
            InitializeComponent();
            _touristService = new TouristService();
            _tourService = new TourService();
        }

        private bool AuthenticateDeletion()
        {
            var userRepository = new UserRepository();
            return PasswordBox.Password == userRepository.GetByUsername(CurrentUser.Username).Password;
        }

        private void CancelTour()
        {
            var tourVoucherRepository = new TourVoucherRepository();
            var deletedTour = _tourService.GetByName(TourNameText.Text);
            var tourists = _touristService.GetByTour(deletedTour);
            var tourDates = deletedTour.Date.Split(", ").ToList();

            foreach (var tourist in tourists)
                tourVoucherRepository.Add(new TourVoucher(tourist.Id, "Valid Voucher", DateTime.ParseExact(DateTime.Now.AddYears(1).ToString("d/M/yyyy"), "d/M/yyyy", CultureInfo.InvariantCulture)));

            if (tourDates.Count < 2)
            {
                foreach (var tourist in tourists)
                    _touristService.RemoveTour(tourist.Id);
                _tourService.Remove(deletedTour.Id);
            }

            else
            {
                var cancelledDate = CancelledTour.Date!;
                _tourService.RemoveDate(cancelledDate, tourDates, deletedTour.Id);
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
