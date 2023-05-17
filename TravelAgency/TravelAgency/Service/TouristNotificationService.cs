using System.Collections.ObjectModel;
using TravelAgency.Dto;
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
                //Debug.WriteLine("-------------------------------");
                //Debug.WriteLine($"suggested: {location} -- request: {request.Location}");
                //Debug.WriteLine($"suggested: {language} -- request: {request.Language}");
                //Debug.WriteLine($"status: {request.Status}");
                //Debug.WriteLine("-------------------------------");
                //Debug.WriteLine(request.Language);
                if (request.Status != Status.Invalid) continue;

                if (!IsLocationEqual(request.Location, location) && request.Language != language) continue;
                //Debug.WriteLine($"Accepted tour: {tourName}\n Location: {request.Location} -- Language: {request.Language} -- status: {request.Status}");

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
