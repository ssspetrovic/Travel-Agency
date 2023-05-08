using System.Windows;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for RateTourView.xaml
    /// </summary>
    public partial class RateTourView
    {
        public RateTourView(string selectedTourName = "/")
        {
            InitializeComponent();
            DataContext = new RateTourViewModel(NavigationService!, selectedTourName);
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MyToursView());
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
