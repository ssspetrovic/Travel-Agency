using System.Windows.Navigation;
using TravelAgency.Command;

namespace TravelAgency.ViewModel.Tourist
{
    internal class ComplexTourRequestViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand AddTourPartCommand { get; set; }

        public ComplexTourRequestViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;
            CancelCommand = new RelayCommand(Execute_CancelCommand);
            SubmitCommand = new RelayCommand(Execute_SubmitCommand, CanExecute_SubmitCommand);
            AddTourPartCommand = new RelayCommand(Execute_AddTourPartCommand, CanExecute_AddTourPartCommand);
        }

        private void Execute_CancelCommand(object parameter)
        {
            _navigationService.GoBack();
        }

        private void Execute_SubmitCommand(object parameter)
        {
            // TODO Implement
        }

        private bool CanExecute_SubmitCommand(object parameter)
        {
            // TODO Implement
            return false;
        }

        private void Execute_AddTourPartCommand(object parameter)
        {
            // TODO Implement
        }

        private bool CanExecute_AddTourPartCommand(object parameter)
        {
            // TODO Implement
            return false;
        }
    }
}
