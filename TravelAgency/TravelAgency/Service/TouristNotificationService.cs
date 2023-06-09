using System.Collections.ObjectModel;
using TravelAgency.DTO;
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

        public ObservableCollection<TouristNotificationDto> GetAllDtoAsCollection()
        {
            return _notificationRepository.GetAllDtoAsCollection();
        }

        public void UpdateStatus(int id, NotificationStatus newStatus)
        {
            _notificationRepository.UpdateStatus(id, newStatus);
        }

        public void UpdateType(int id, NotificationType newType)
        {
            _notificationRepository.UpdateType(id, newType);
        }

        private bool IsLocationEqual(Location firstLocation, Location secondLocation)
        {
            return firstLocation.City == secondLocation.City && firstLocation.Country == secondLocation.Country;
        }

        public void NotifyForNewTours(string tourName, Location location, Language language)
        {
            var requestTourService = new RequestTourService();
            var allRequests = requestTourService.GetAllForSelectedYearAsCollection();

            foreach (var request in allRequests)
            {
                if (request.Status != Status.Invalid) continue;

                if (!IsLocationEqual(request.Location, location) && request.Language != language) continue;

                Add(new TouristNotification(
                    request.TouristUsername!,
                    $"You have received a suggestion for tour: {tourName}",
                    tourName,
                    NotificationStatus.Unread,
                    NotificationType.NewOffer
                ));
            }
        }

        private bool IsDuplicate(string? username, string? text)
        {
            return _notificationRepository.IsDuplicate(username, text);
        }

        public void CheckForAttendanceInvitation(string touristUsername)
        {
            var touristService = new TouristService();
            var tourist = touristService.GetByUsername(touristUsername);

            if (tourist.TouristAppearance != TouristAppearance.Pinged) return;
            
            var notificationText = $"Guide is asking you to check in for '{tourist.Tour.Name}' tour!";

            if (IsDuplicate(touristUsername, notificationText)) return ;

            var notificationService = new TouristNotificationService();
            notificationService.Add(new TouristNotification(
                tourist.UserName,
                notificationText,
                tourist.Tour.Name,
                NotificationStatus.Unread,
                NotificationType.Attendance
            ));
        }
    }
}
