using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Model;
using TravelAgency.Repository;

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
            MessageBox.Show(PasswordBox.Password + "    " + userRepository.GetByUsername(CurrentUser.Username).Password);
            return PasswordBox.Password == userRepository.GetByUsername(CurrentUser.Username).Password;
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!AuthenticateDeletion() || CancelledTour.Date == null)
            {
                MessageBox.Show("Tour cancel was not successful");
            }
            else
            {
                var tourRepository = new TourRepository();
                var touristRepository = new TouristRepository();
                var deletedTour = tourRepository.GetByName(TourNameText.Text);
                var tourists = touristRepository.GetByTour(deletedTour);
                var tourDates = tourists[0].Tour.Date.Split(", ").ToList();

                if (tourDates.Count < 2)
                {
                    foreach (var tourist in tourists)
                        touristRepository.RemoveTour(tourist.Id);

                    tourRepository.Remove(deletedTour.Id);
                }

                else
                {
                    var cancelledDate = CancelledTour.Date;
                    tourRepository.RemoveDate(cancelledDate, tourDates, tourists[0].Tour.Id);
                }
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
