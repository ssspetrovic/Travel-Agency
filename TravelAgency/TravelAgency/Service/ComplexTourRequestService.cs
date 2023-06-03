
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    internal class ComplexTourRequestService
    {
        private readonly ComplexTourRequestRepository _complexTourRequestRepository;

        public ComplexTourRequestService()
        {
            _complexTourRequestRepository = new ComplexTourRequestRepository();
        }

        public int GetLastId()
        {
            return _complexTourRequestRepository.GetLastId();
        }
    }
}
