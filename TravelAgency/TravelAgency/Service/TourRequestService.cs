using System.Collections.ObjectModel;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    internal class TourRequestService
    {
        private readonly RegularTourRequestRepository _tourRequestRepository;

        public void Add(RegularTourRequest tourRequest)
        {
            _tourRequestRepository.AddRegular(tourRequest);
        }

        public void UpdateStatus(int id, RegularTourRequest.TourRequestStatus newStatus)
        {
            _tourRequestRepository.UpdateStatusRegular(id, newStatus);
        }

        public ObservableCollection<RegularTourRequest> GetAllAsCollection()
        {
            return _tourRequestRepository.GetAllRegularAsCollection();
        }

        public TourRequestService()
        {
            _tourRequestRepository = new RegularTourRequestRepository();
        }
    }
}
