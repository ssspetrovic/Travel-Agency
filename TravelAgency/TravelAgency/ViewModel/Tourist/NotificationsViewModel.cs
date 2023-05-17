using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Tourist;
using static System.Windows.Application;

namespace TravelAgency.ViewModel.Tourist
{
    internal class NotificationsViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly TouristViewModel _touristViewModel;
        private readonly TouristNotificationService _touristNotificationService;

        private Tour? Tour { get; set; }
        private bool IsAttendanceAccepted { get; set; }
        private AcceptInvitationDialog Dialog { get; set; }

        public RelayCommand ViewNotificationCommand { get; set; }
        public RelayCommand DeleteNotificationCommand { get; set; }
        public RelayCommand ConfirmAttendanceCommand { get; set; }
        public RelayCommand CancelAttendanceCommand { get; set; }

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

            Dialog = new AcceptInvitationDialog();

            ViewNotificationCommand = new RelayCommand(Execute_ViewNotificationCommand, CanExecute_ViewNotificationCommand);
            DeleteNotificationCommand = new RelayCommand(Execute_DeleteNotificationCommand, CanExecute_DeleteNotificationCommand);
            ConfirmAttendanceCommand = new RelayCommand(Execute_ConfirmAttendanceCommand);
            CancelAttendanceCommand = new RelayCommand(Execute_CancelAttendanceCommand);

            _notificationsCollection = _touristNotificationService.GetAllDtoAsCollection();
        }

        private void Execute_ViewNotificationCommand(object parameter)
        {
            if (parameter is not TouristNotification notification) return;

            if (notification.Type == NotificationType.NewOffer)
            {
                var tourService = new TourService();
                Tour = tourService.GetByName(notification.TourName);
                _navigationService.Navigate(new TourView(Tour));
            }
            else
            {
                var touristService = new TouristService();

                if (!IsAttendanceConfirmed(notification.TourName)) return;
                touristService.UpdateAppearanceByUsername(notification.TouristUsername, TouristAppearance.Present);

                var myTourDtoService = new MyTourDtoService();
                myTourDtoService.UpdateStatus(notification.TourName, MyTourDto.TourStatus.Attending);
            }
        }

        private bool CanExecute_ViewNotificationCommand(object parameter)
        {
            if (parameter is TouristNotification notification)
                return notification.Type != NotificationType.NewVoucher;

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

        private void Execute_ConfirmAttendanceCommand(object parameter)
        {
            IsAttendanceAccepted = true;
            Dialog.Close();
        }

        private void Execute_CancelAttendanceCommand(object parameter)
        {
            IsAttendanceAccepted = false;
            Dialog.Close();
        }

        private bool IsAttendanceConfirmed(string notificationText)
        {
            Dialog = new AcceptInvitationDialog
            {
                Owner = Current.MainWindow,
                DataContext = this,
                NotificationTextBlock = { Text = notificationText },
                CancelButton = { Command = CancelAttendanceCommand },
                ConfirmButton = { Command = ConfirmAttendanceCommand }
            };

            Dialog.ShowDialog();
            return IsAttendanceAccepted;
        }
    }
}
