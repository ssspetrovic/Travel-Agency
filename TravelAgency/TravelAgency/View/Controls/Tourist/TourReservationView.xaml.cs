using System.Windows;
using System.Windows.Controls.Primitives;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for TourReservationView.xaml
    /// </summary>
    public partial class TourReservationView
    {
        private readonly TourReservationViewModel _viewModel = new();

        public TourReservationView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
     
        private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            Close();
        }

        private void HeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var touristView = new TouristView();
            touristView.Show();
            Close();
        }

        private void MakeReservationButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.MakeReservation();
        }

        private void ApplyFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ApplyFilter();
        }

        private void ResetFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ResetFilter();
        }
    }
}
