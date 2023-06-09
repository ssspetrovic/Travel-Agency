using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.View.Tourist;

namespace TravelAgency.ViewModel.Tourist
{
    internal class RequestTourViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;
        public RelayCommand NavigateToRegularTourRequestCommand { get; set; }
        public RelayCommand NavigateToComplexTourRequestCommand { get; set; }
        public RelayCommand NavigateToMyRequestsCommand { get; set; }

        public RequestTourViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;
            NavigateToRegularTourRequestCommand = new RelayCommand(Execute_NavigateToRegularTourRequestCommand);
            NavigateToComplexTourRequestCommand = new RelayCommand(Execute_NavigateToComplexTourRequestCommand);
            NavigateToMyRequestsCommand = new RelayCommand(Execute_NavigateToMyRequestsCommand);
        }

        private void Execute_NavigateToRegularTourRequestCommand(object parameter)
        {
            _navigationService.Navigate(new RegularTourRequestView(_navigationService, _touristViewModel));
        }
        
        private void Execute_NavigateToComplexTourRequestCommand(object parameter)
        {
            _navigationService.Navigate(new ComplexTourRequestView(_navigationService, _touristViewModel));
        }

        private void Execute_NavigateToMyRequestsCommand(object parameter)
        {
            _touristViewModel.IsMyRequestsRadioButtonChecked = true;
            _navigationService.Navigate(new MyRequestsView(_navigationService));
        }
    }
}
