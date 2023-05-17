using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Dto;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Tourist;

namespace TravelAgency.ViewModel.Tourist
{
    class NotificationsViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;
        private readonly TouristNotificationService _touristNotificationService;

        private Tour? Tour { get; set; }
        public RelayCommand ViewNotificationCommand { get; set; }
        public RelayCommand DeleteNotificationCommand { get; set; }

        private ObservableCollection<TouristNotificationDto> _notificationsCollection;
        public ObservableCollection<TouristNotificationDto> NotificationsCollection
        {
            get => _notificationsCollection;
            set
            {
                _notificationsCollection = value;
                OnPropertyChanged();
            }
        }

        private bool _isMainCheckBoxChecked;
        public bool IsMainCheckBoxChecked
        {
            get => _isMainCheckBoxChecked;
            set
            {
                _isMainCheckBoxChecked = value;

                foreach (var item in NotificationsCollection)
                    item.IsChecked = _isMainCheckBoxChecked;

                OnPropertyChanged();
            }
        }

        public NotificationsViewModel(NavigationService navigationService, TouristViewModel touristViewModel)
        {
            _navigationService = navigationService;
            _touristViewModel = touristViewModel;
            _touristNotificationService = new TouristNotificationService();

            ViewNotificationCommand = new RelayCommand(Execute_ViewNotificationCommand, CanExecute_ViewNotificationCommand);
            DeleteNotificationCommand = new RelayCommand(Execute_DeleteNotificationCommand, CanExecute_DeleteNotificationCommand);

            _notificationsCollection = _touristNotificationService.GetAllDtoAsCollection();
        }

        private void Execute_ViewNotificationCommand(object parameter)
        {
            if (parameter is not TouristNotification notification) return;

            var tourService = new TourService();
            Tour = tourService.GetByName(notification.TourName);
            _navigationService.Navigate(new TourView(Tour));
        }
        private bool CanExecute_ViewNotificationCommand(object parameter)
        {
            if (parameter is TouristNotification notification)
                return notification.Type == NotificationType.NewOffer;

            return false;
        }

        private void Execute_DeleteNotificationCommand(object parameter)
        {
            foreach (var notification in _notificationsCollection.ToList().Where(notification => notification.IsChecked))
            {
                _touristNotificationService.DeleteById(notification.Id);
                _notificationsCollection.Remove(notification);
            }

            CollectionViewSource.GetDefaultView(_notificationsCollection).Refresh();
        }

        private bool CanExecute_DeleteNotificationCommand(object parameter)
        {
            return _notificationsCollection.Any(notification => notification.IsChecked);
        }
    }
}
