using System;
using System.Collections.Generic;
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
using TravelAgency.Service;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationView.xaml
    /// </summary>
    public partial class RegisterAccommodationView : Page
    {
        int validate;
        public RegisterAccommodationView()
        {
            InitializeComponent();
            if (CurrentLanguageAndTheme.themeId == 0)
            {
                cmbLocation.Background = Brushes.White; cmbLocation.Foreground = Brushes.Black;
                cmbType.Background = Brushes.White; cmbType.Foreground = Brushes.Black;
                foreach (ComboBoxItem item in cmbLocation.Items)
                {
                    item.Background = Brushes.White; item.Foreground = Brushes.Black;
                }
                foreach (ComboBoxItem item in cmbType.Items)
                {
                    item.Background = Brushes.White; item.Foreground = Brushes.Black;
                }
            }
            else
            {
                cmbLocation.Background = Brushes.Black; cmbLocation.Foreground = Brushes.White;
                cmbType.Background = Brushes.Black; cmbType.Foreground = Brushes.White;
                foreach (ComboBoxItem item in cmbLocation.Items)
                {
                    item.Background = Brushes.Black; item.Foreground = Brushes.White;
                }
                foreach (ComboBoxItem item in cmbType.Items)
                {
                    item.Background = Brushes.Black; item.Foreground = Brushes.White;
                }
            }

            lblNameINC.Visibility = Visibility.Hidden;
            lblResINC.Visibility = Visibility.Hidden;
            lblMaxINC.Visibility = Visibility.Hidden;
            lblMinINC.Visibility = Visibility.Hidden;
            lblDescINC.Visibility = Visibility.Hidden;
            lblAddressINC.Visibility = Visibility.Hidden;
        }

        public RegisterAccommodationView(string city)
        {
            InitializeComponent();
            if (CurrentLanguageAndTheme.themeId == 0)
            {
                cmbLocation.Background = Brushes.White; cmbLocation.Foreground = Brushes.Black;
                cmbType.Background = Brushes.White; cmbType.Foreground = Brushes.Black;
                foreach (ComboBoxItem item in cmbLocation.Items)
                {
                    item.Background = Brushes.White; item.Foreground = Brushes.Black;
                }
                foreach (ComboBoxItem item in cmbType.Items)
                {
                    item.Background = Brushes.White; item.Foreground = Brushes.Black;
                }
            }
            else
            {
                cmbLocation.Background = Brushes.Black; cmbLocation.Foreground = Brushes.White;
                cmbType.Background = Brushes.Black; cmbType.Foreground = Brushes.White;
                foreach (ComboBoxItem item in cmbLocation.Items)
                {
                    item.Background = Brushes.Black; item.Foreground = Brushes.White;
                }
                foreach (ComboBoxItem item in cmbType.Items)
                {
                    item.Background = Brushes.Black; item.Foreground = Brushes.White;
                }
            }

            lblNameINC.Visibility = Visibility.Hidden;
            lblResINC.Visibility = Visibility.Hidden;
            lblMaxINC.Visibility = Visibility.Hidden;
            lblMinINC.Visibility = Visibility.Hidden;
            lblDescINC.Visibility = Visibility.Hidden;
            lblAddressINC.Visibility = Visibility.Hidden;

            cmbLocation.Text = city;
        }

        private AccommodationType findAccommodationType(string text)
        {
            if (text == "Apartment" || text == "Apartman")
                return AccommodationType.Apartment;
            else if (text == "House" || text == "Kuća")
                return AccommodationType.House;
            else
                return AccommodationType.Cottage;
        }
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            validate = 0;
            ValidateInput();
            if (validate != 0)
                return;

            var accommodationRepository = new AccommodationRepository();

            var name = txtName.Text;
            var locationService = new LocationService();
            var currentLocation = locationService.GetByCity(cmbLocation.Text);
            var address = txtAdress.Text;
            var type = findAccommodationType(cmbType.Text);
            var minDaysReservation = Convert.ToInt32(txtMinReservationDays.Text);
            var maxDaysReservation = Convert.ToInt32(txtMaxReservationDays.Text);
            var reservableDays = Convert.ToInt32(txtReservableDays.Text);
            var description = txtDescription.Text;
            var ownerId = CurrentUser.Id;

            if(minDaysReservation > maxDaysReservation)
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("Minimum number of guests is higher that the maximum!", "Message");
                else
                    MessageBox.Show("Minimalni broj gostiju je veci od maksimuma!", "Poruka");
                return;
            }

            Accommodation accommodation = new Accommodation(name, currentLocation!.Id, type, minDaysReservation, maxDaysReservation, address, reservableDays,
                ImagesList.Text, description, ownerId);
            try
            {

                accommodationRepository.Add(accommodation);
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("Accommodation registered successfuly!", "Message");
                else
                    MessageBox.Show("Smeštaj uspeštno registrovan!", "Poruka");
                Refresh();
            }
            catch
            {
                if (CurrentLanguageAndTheme.languageId == 0)
                    MessageBox.Show("Error while registering!", "Message");
                else
                    MessageBox.Show("Greška prilikom registracije!", "Poruka");
            }
        }

        private void txtAddUrl_Click(object sender, RoutedEventArgs e)
        {
            var hasText = false;
            if (ImagesList.Text.Contains(txtImage.Text))
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                ImagesList.Text.Replace(txtImage.Text, "");
                hasText = true;
            }

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            if (ImagesList.Text.Contains(",, "))
                ImagesList.Text.Replace(",, ", ", ");

            if (!hasText)
                if (ImagesList.Text.Length == 0)
                    ImagesList.Text += txtImage.Text;
                else
                    ImagesList.Text += ", " + txtImage.Text;

            if (ImagesList.Text.StartsWith(", "))
                ImagesList.Text = ImagesList.Text.Substring(2, ImagesList.Text.Length - 2);

            txtImage.Text = "";
        }

        private void btnClearImages_Click(object sender, RoutedEventArgs e)
        {
            ImagesList.Text = "";
        }

        private void NameValidation()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblNameINC.Visibility = Visibility.Visible;
                validate++;
            }
            else
            {
                lblNameINC.Visibility = Visibility.Hidden;
            }
        }

        private void AddressValidation()
        {
            if (string.IsNullOrWhiteSpace(txtAdress.Text))
            {
                lblAddressINC.Visibility = Visibility.Visible;
                validate++;
            }
            else
            {
                lblAddressINC.Visibility = Visibility.Hidden;
            }
        }

        private void DescriptionValidation()
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                lblDescINC.Visibility = Visibility.Visible;
                validate++;
            }
            else
            {
                lblDescINC.Visibility = Visibility.Hidden;
            }
        }

        private void MinGuestValidation()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMinReservationDays.Text))
                {
                    lblMinINC.Visibility = Visibility.Visible;
                    validate++;
                    return;
                }
                int number = Convert.ToInt32(txtMinReservationDays.Text);
                if (number < 1)
                {
                    lblMinINC.Visibility = Visibility.Visible;
                    validate++;
                    return;
                }
                else
                    lblMinINC.Visibility = Visibility.Hidden;
            }
            catch
            {
                lblMinINC.Visibility = Visibility.Visible;
                validate++;
            }
        }

        private void MaxGuestValidation()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaxReservationDays.Text))
                {
                    lblMaxINC.Visibility = Visibility.Visible;
                    validate++;
                    return;
                }
                int number = Convert.ToInt32(txtMaxReservationDays.Text);
                if (number < 1)
                {
                    lblMaxINC.Visibility = Visibility.Visible;
                    validate++;
                    return;
                }
                else
                    lblMaxINC.Visibility = Visibility.Hidden;
            }
            catch
            {
                lblMaxINC.Visibility = Visibility.Visible;
                validate++;
            }
        }

        private void ReservableDaysValidation()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReservableDays.Text))
                {
                    lblResINC.Visibility = Visibility.Visible;
                    validate++;
                    return;
                }
                int number = Convert.ToInt32(txtReservableDays.Text);
                if (number < 1)
                {
                    lblResINC.Visibility = Visibility.Visible;
                    validate++;
                    return;
                }
                else
                    lblResINC.Visibility = Visibility.Hidden;
            }
            catch
            {
                lblResINC.Visibility = Visibility.Visible;
                validate++;
            }
        }

        private void ValidateInput()
        {
            NameValidation();
            AddressValidation();
            MinGuestValidation();
            MaxGuestValidation();
            ReservableDaysValidation();
            DescriptionValidation();
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
            RegisterAccommodationView registerAccommodationView = new RegisterAccommodationView();
            mainFrame.Navigate(registerAccommodationView);
        }
    }
}
