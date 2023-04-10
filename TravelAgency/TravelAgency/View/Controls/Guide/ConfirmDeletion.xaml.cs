using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            PasswordBox.Focus();
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
                tourVoucherRepository.Add(new TourVoucher(tourist.Id, tourist.UserName, "Valid Voucher", DateTime.ParseExact(DateTime.Now.AddYears(1).ToString("d/M/yyyy"), "d/M/yyyy", CultureInfo.InvariantCulture)));

            if (tourDates.Count < 2)
            {
                foreach (var tourist in tourists)
                {
                    _touristService.RemoveTour(tourist.Id);
                    _touristService.UpdateAppearance(tourist.Id, TouristAppearance.Unknown);
                }
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


        private List<RadioButton> InitializeRadioButtonData()
        {
            var dateOptionsList = new List<RadioButton>();
            foreach (var item in DateOptionsItemsControl.Items)
            {
                var container = DateOptionsItemsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                var radioButton = container!.ContentTemplate.FindName("DateOptions", container) as RadioButton;
                dateOptionsList.Add(radioButton!);
            }

            return dateOptionsList;
        }

        private void ConfirmDeletion_OnKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
                ConfirmButton_OnClick(sender, e);
            if (e.Key == Key.Escape)
                Close();

            var dateOptionsList = InitializeRadioButtonData();

            if (e.Key is >= Key.F1 and <= Key.F12)
            {
                var index = e.Key - Key.F1;
                if (index < dateOptionsList.Count)
                {
                    var radioButton = dateOptionsList[index];
                    radioButton.IsChecked = true;
                    e.Handled = true;
                }
            }
        }
    }
}
