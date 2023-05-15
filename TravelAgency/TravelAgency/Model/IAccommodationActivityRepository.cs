using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface IAccommodationActivityRepository
    {
        void Add(AccommodationActivity activity);

        List<AccommodationActivity> GetAllByAccommodationId(int accommodationId);
    }
}
