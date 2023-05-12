using System.Windows;
using System.Windows.Navigation;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.View.Tourist
{
    /// <summary>
    /// Interaction logic for RequestTourView.xaml
    /// </summary>
    public partial class RequestTourView
    {
        private readonly TouristViewModel _touristViewModel;
        private readonly NavigationService _navigationService;
        public RequestTourView(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;
        }

        private void RegularTourRequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegularTourRequestView(_navigationService, _touristViewModel));
        }
    }
}
