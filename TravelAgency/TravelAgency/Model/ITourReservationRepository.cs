using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface ITourReservationRepository
    {
        void Add(ActiveTourModel activeTourModel);
        void Remove();
        ActiveTourModel GetById(int id);
    }
}
