using System.Collections.ObjectModel;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    public interface ITourReservationRepository
    {
        void Add(TourReservation tourReservation);
        void DeleteById(int id);
        Collection<TourReservation> GetAllAsCollection();
        TourReservation GetById(int id);
    }
}
