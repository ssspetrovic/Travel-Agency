using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface ITourReservationRepository
    {
        void Add(TourReservation tourReservation);
        void Remove();
        TourReservation GetById(int id);
    }
}
