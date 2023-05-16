using System.Collections.ObjectModel;
using System.Windows.Navigation;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    class NotificationsViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;
        private readonly TouristNotificationService _touristNotificationService;

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
            _touristNotificationService = new TouristNotificationService();
            _notificationsCollection = _touristNotificationService.GetAllAsCollection();
        }
    }
}
