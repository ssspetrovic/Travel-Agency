using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public void NotifyForNewTours(string tourName, Location location, Language language)
        {
            Debug.WriteLine("in");
            Debug.WriteLine(tourName);
            Debug.WriteLine(location);
            Debug.WriteLine(language);

            var requestTourService = new RequestTourService();
            var allRequests = requestTourService.GetAllForSelectedYearAsCollection();

            foreach (var request in allRequests)
            {
                Debug.WriteLine(request.Location);
                Debug.WriteLine(request.Status);
                Debug.WriteLine(request.Language);
                if (request.Status != Status.Invalid) continue;

                if (request.Location != location && request.Language != language) continue;
                Debug.WriteLine("true");

                Add(new TouristNotification(
                    request.TouristUsername!,
                    $"You received a suggestion for tour: {tourName}",
                    tourName,
                    NotificationStatus.Unread,
                    NotificationType.NewOffer
                ));
            }
        }
    }
}
