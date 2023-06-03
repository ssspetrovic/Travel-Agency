
using System.Collections.ObjectModel;
using TravelAgency.Model;
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

        public void Add(ComplexRequestTour complexTourRequest)
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

        public int GetLastId()
        {
            return _complexTourRequestRepository.GetLastId();
        }
    }
}
