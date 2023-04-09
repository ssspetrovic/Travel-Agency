using System.Collections.ObjectModel;

namespace TravelAgency.Model
{
    internal interface ITourReservationRepository
    {
        void Add(TourReservation tourReservation);
        void DeleteById(int id);
        TourReservation GetById(int id);
        Collection<TourReservation> GetAllAsCollection();

    }
}
