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

        public RegularTourRequestService()
        {
            _tourRequestRepository = new RegularTourRequestRepository();
        }
    }
}
