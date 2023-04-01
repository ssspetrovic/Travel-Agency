using System;
using System.Windows;
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
            return PasswordBox.Password == userRepository.GetByUsername(CurrentUser.Username).Password;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (AuthenticateDeletion())
            {
                MessageBox.Show("Tour cancel was not successful");
            }
            else
            {
                var tourRepository = new TourRepository();
                var touristRepository = new TouristRepository();
                var deletedTour = tourRepository.GetByName(TourNameText.Text);
                var tourists = touristRepository.GetByTour(deletedTour);

                foreach (var tourist in tourists)
                    touristRepository.RemoveTour(tourist.Id);
                
                tourRepository.Remove(deletedTour.Id);
            }

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
