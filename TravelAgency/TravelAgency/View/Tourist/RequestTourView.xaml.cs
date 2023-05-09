using System.Windows;
using System.Windows.Navigation;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for RequestTourView.xaml
    /// </summary>
    public partial class RequestTourView
    {
        private readonly NavigationService _navigationService;
        public RequestTourView(NavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }

        private void RegularTourRequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegularTourRequestView(_navigationService));
        }
    }
}
