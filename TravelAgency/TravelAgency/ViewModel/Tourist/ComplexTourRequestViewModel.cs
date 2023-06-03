using System.Windows.Navigation;
using TravelAgency.Command;

namespace TravelAgency.ViewModel.Tourist
{
    internal class ComplexTourRequestViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;

        public RelayCommand CancelCommand { get; set; }

        public ComplexTourRequestViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;
            CancelCommand = new RelayCommand(Execute_CancelCommand);
        }

        private void Execute_CancelCommand(object parameter)
        {
            _navigationService.GoBack();
        }
    }
}
