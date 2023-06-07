using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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

        private void UpdateStatus(int id, Status newStatus)
        {
            _complexTourRequestRepository.UpdateStatus(id, newStatus);
        }

        private RequestTour GetFirstTourPart(ComplexRequestTour complexRequest)
        {
            var firstTourPartId = int.Parse(complexRequest.RequestTourIds.Split(", ")[0]);
            var firstTourPart = _regularTourRequestService.GetById(firstTourPartId);
            return firstTourPart;
        }

        private DateTime GetStartingDateTime(RequestTour tourPart)
        {
            var startingDateText = tourPart.DateRange.Split(" - ")[0];
            var startingDate = DateTime.ParseExact(startingDateText, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            return startingDate;
        }

        private bool IsComplexExpired(ComplexRequestTour complexRequest)
        {
            var firstTourPart = GetFirstTourPart(complexRequest);
            var startingDate = GetStartingDateTime(firstTourPart);

            return startingDate <= DateTime.Now.AddHours(48) && firstTourPart.Status != Status.Accepted;
        }

        private bool IsComplexAccepted(ComplexRequestTour complexRequest)
        {
            var partIdsStr = complexRequest.RequestTourIds.Split(", ");
            var partIds = partIdsStr.Select(int.Parse).ToArray();

            return partIds.Select(id => _regularTourRequestService.GetById(id)).All(part => part.Status == Status.Accepted);
        }

        public void HandleComplexStatuses()
        {
            var complexRequests = GetAllAsCollection();
            foreach (var complexRequest in complexRequests)
            {
                if (IsComplexExpired(complexRequest))
                {
                    UpdateStatus(complexRequest.Id, Status.Invalid);
                }

                if (IsComplexAccepted(complexRequest))
                {
                    UpdateStatus(complexRequest.Id, Status.Accepted);
                }
            }
        }
    }
}
