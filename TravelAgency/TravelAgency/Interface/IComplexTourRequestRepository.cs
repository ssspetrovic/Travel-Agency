using System.Collections.ObjectModel;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    interface IComplexTourRequestRepository
    {
        void Add(TourVoucher tourVoucher);
        void DeleteById(int id);
        ObservableCollection<ComplexRequestTour> GetAllAsCollection();
        public int GetLastId();
    }
}
