using System;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.View.Controls.Guide
{
    public partial class Resign
    {

        private readonly UserService _userService;
        private readonly TourService _tourService;
        private readonly TouristService _touristService;
        private readonly TourVoucherService _tourVoucherService;

        public Resign()
        {
            InitializeComponent();
            _userService = new UserService();
            _tourService = new TourService();
            _touristService = new TouristService();
            _tourVoucherService = new TourVoucherService();
        }

        private void ResignButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!AuthenticateResign())
            {
                MessageBox.Show("Authentication was not successful");
                return;
            }
            _userService.RemoveByUsername(CurrentUser.Username);
            var tours = _tourService.GetAllByUsername(CurrentUser.Username);

            foreach (var tour in tours)
            {
                var tourists = _touristService.GetByTour(tour);
                foreach (var tourist in tourists)
                    _tourVoucherService.Add(new TourVoucher(tourist.Id, tourist.UserName, "Voucher for resigned guide", DateTime.Now.AddYears(2), TourVoucher.VoucherStatus.Valid));
            }

            MessageBox.Show("You have successfully removed your account!");
            var loginView = new SignInView();
            loginView.Show();
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            var guideView = new View.Guide();
            guideView.Show();
            Close();
        }

        private bool AuthenticateResign()
        {
            if (PasswordBox.Password != PasswordBoxConfirm.Password)
                return false;
            if (PasswordBox.Password != _userService.GetByUsername(CurrentUser.Username).Password)
                return false;
            return true;
        }
    }
}
