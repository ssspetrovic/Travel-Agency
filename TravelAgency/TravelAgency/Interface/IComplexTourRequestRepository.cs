using System.Collections.ObjectModel;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    internal interface IComplexTourRequestRepository
    {
        void Add(ComplexRequestTour complexTourRequest);
        void DeleteById(int id);
        ObservableCollection<ComplexRequestTour> GetAllAsCollection();
        public int GetLastId();
    }
}
