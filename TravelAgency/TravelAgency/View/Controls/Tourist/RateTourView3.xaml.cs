using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for RateTourView.xaml
    /// </summary>
    public partial class RateTourView3
    {
        public RateTourView3(string tourName = "/")
        {
            InitializeComponent();
            DataContext = new RateTourViewModel(tourName);
        }

        private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TourReservationButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToursButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

        }

        private void MyVouchersButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((RateTourViewModel)DataContext).Submit();
        }

        private void AddUrlButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((RateTourViewModel)DataContext).AddUrl();
        }
    }
}
