using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for TouristView.xaml
    /// </summary>
    public partial class TouristView
    {
        public TouristView()
        {
            InitializeComponent();
            //DataContext = new TouristViewModel();
            ContentFrame.Source = new Uri("Controls/Tourist/HomeView.xaml", UriKind.Relative);
            HomeButton.IsChecked = true;
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left += e.HorizontalChange;
            Top += e.VerticalChange;
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Source = new Uri("Controls/Tourist/HomeView.xaml", UriKind.Relative);
            HomeButton.IsChecked = true;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TourReservationButton_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Source = new Uri("Controls/Tourist/TourReservationView.xaml", UriKind.Relative);
            TourReservationButton.IsChecked = true;
        }

        private void MyToursButton_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Source = new Uri("Controls/Tourist/MyToursView.xaml", UriKind.Relative);
            MyToursButton.IsChecked = true;
        }

        private void MyVouchersButton_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Source = new Uri("Controls/Tourist/MyTourVouchersView.xaml", UriKind.Relative);
            MyVouchersButton.IsChecked = true;
        }

        private void RequestTourButton_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFrame.Source = new Uri("Controls/Tourist/RequestTourView.xaml", UriKind.Relative);
            RequestTourButton.IsChecked = true;
        }

        private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }
    }
}
