using System.Collections.ObjectModel;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    class NotificationsViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;
        public RelayCommand ViewNotificationCommand { get; set; }
        private ObservableCollection<TouristNotification> _notificationsCollection;
        public ObservableCollection<TouristNotification> NotificationsCollection
        {
            get => _notificationsCollection;
            set
            {
                _notificationsCollection = value;
                OnPropertyChanged();
            }
        }

        public NotificationsViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;
            
            ViewNotificationCommand = new RelayCommand(Execute_ViewNotificationCommand);

            var touristNotificationService = new TouristNotificationService();
            _notificationsCollection = touristNotificationService.GetAllAsCollection();
        }

        private void Execute_ViewNotificationCommand(object parameter)
        {
            if (parameter is TouristNotification notification)
            {
                // TODO Implement method
            }
        }
    }
}
