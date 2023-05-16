using System.Collections.ObjectModel;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    class TouristNotificationService
    {
        private readonly TouristNotificationRepository _notificationRepository;

        public TouristNotificationService()
        {
            _notificationRepository = new TouristNotificationRepository();
        }

        public void Add(TouristNotification notification)
        {
            _notificationRepository.Add(notification);
        }

        public void DeleteById(int id)
        {
            _notificationRepository.DeleteById(id);
        }

        public ObservableCollection<TouristNotification> GetAllAsCollection()
        {
            return _notificationRepository.GetAllAsCollection();
        }

        public void UpdateStatus(int id, NotificationStatus newStatus)
        {
            _notificationRepository.UpdateStatus(id, newStatus);
        }

        public void NotifyForNewTours(Location location, Language language)
        {
            var requestTourService = new RequestTourService();

        }
    }
}
