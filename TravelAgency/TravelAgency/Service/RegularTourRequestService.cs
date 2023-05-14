using System.Collections.ObjectModel;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    internal class RegularTourRequestService
    {
        private readonly RegularTourRequestRepository _tourRequestRepository;

        public void Add(RegularTourRequest tourRequest)
        {
            _tourRequestRepository.Add(tourRequest);
        }

        public void UpdateStatus(int id, RegularTourRequest.TourRequestStatus newStatus)
        {
            _tourRequestRepository.UpdateStatus(id, newStatus);
        }

        public ObservableCollection<RegularTourRequest> GetAllForSelectedYearAsCollection(string? year = null)
        {
            return _tourRequestRepository.GetAllForSelectedYearAsCollection(year);
        }

        public ObservableCollection<string> GetAllYears()
        {
            return _tourRequestRepository.GetAllYears();
        }

        public RegularTourRequestService()
        {
            _tourRequestRepository = new RegularTourRequestRepository();
        }
    }
}
