using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    internal class ComplexTourRequestService
    {
        private readonly ComplexTourRequestRepository _complexTourRequestRepository;
        private readonly RequestTourService _regularTourRequestService;

        public ComplexTourRequestService()
        {
            _complexTourRequestRepository = new ComplexTourRequestRepository();
            _regularTourRequestService = new RequestTourService();
        }

        private void Add(ComplexRequestTour complexTourRequest)
        {
            _complexTourRequestRepository.Add(complexTourRequest);
        }

        public void DeleteById(int id)
        {
            _complexTourRequestRepository.DeleteById(id);
        }

        public ObservableCollection<ComplexRequestTour> GetAllAsCollection()
        {
            return _complexTourRequestRepository.GetAllAsCollection();
        }

        private int GetLastId()
        {
            return _complexTourRequestRepository.GetLastId();
        }

        private void AddTourParts(ObservableCollection<RequestTour> tourParts, int complexId)
        {
            tourParts = new ObservableCollection<RequestTour>(tourParts.Select(tourPart =>
            {
                tourPart.ComplexId = complexId;
                return tourPart;
            }));


            foreach (var tourPart in tourParts)
            {
                _regularTourRequestService.Add(tourPart);
            }
        }

        private void CreateComplexTour(IReadOnlyList<RequestTour> tourParts, int complexId)
        {
            var requestedTourIds = _regularTourRequestService.GetRequestedTourIdsById(complexId);
            var dateRange = tourParts[0].DateRange;
            var location = tourParts[0].Location;
            var description = tourParts[0].Description;
            var language = tourParts[0].Language;
            var totalGuests = tourParts.Sum(t => t.MaxGuests);
            var acceptedDate = string.Join(", ", Enumerable.Repeat("/", tourParts.Count));

            Add(
                new ComplexRequestTour(
                    requestedTourIds,
                    dateRange,
                    location,
                    description!,
                    language,
                    totalGuests,
                    Status.OnHold,
                    acceptedDate,
                    CurrentUser.Username!
                )
            );
        }

        public void HandleComplexRequest(ObservableCollection<RequestTour> tourParts)
        {
            var complexId = GetLastId();
            AddTourParts(tourParts, complexId);
            CreateComplexTour(tourParts, complexId);
        }
    }
}
