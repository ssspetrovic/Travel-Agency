using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Tourist;

namespace TravelAgency.ViewModel.Tourist
{
    internal class ComplexTourRequestViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;
        private ObservableCollection<RequestTour> _tourParts;

        public ObservableCollection<RequestTour> TourParts
        {
            get => _tourParts;
            set
            {
                _tourParts = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand AddTourPartCommand { get; set; }
        public RelayCommand RemoveTourPartCommand { get; set; }

        public ComplexTourRequestViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;

            //var requestTourService = new RequestTourService();
            //_tourParts = requestTourService.GetAllForSelectedYearAsCollection();
            _tourParts = new ObservableCollection<RequestTour>();

            CancelCommand = new RelayCommand(Execute_CancelCommand);
            SubmitCommand = new RelayCommand(Execute_SubmitCommand, CanExecute_SubmitCommand);
            AddTourPartCommand = new RelayCommand(Execute_AddTourPartCommand, CanExecute_AddTourPartCommand);
            RemoveTourPartCommand = new RelayCommand(Execute_RemoveTourPartCommand);
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

        private void Execute_RemoveTourPartCommand(object parameter)
        {
            if (parameter is not RequestTour tourPart) return;
            TourParts.Remove(tourPart);
            CollectionViewSource.GetDefaultView(TourParts).Refresh();
        }
    }
}
