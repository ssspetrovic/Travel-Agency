using System.Windows;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for MyToursView.xaml
    /// </summary>
    public partial class MyToursView
    {
        public MyToursView()
        {
            InitializeComponent();
            DataContext = new MyToursViewModel();
        }

        private void JoinTourButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((MyToursViewModel)DataContext).MyTourDtoService.JoinTour();
        }

        private void RateTourButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((MyToursViewModel)DataContext).RateTour();
        }
    }
}