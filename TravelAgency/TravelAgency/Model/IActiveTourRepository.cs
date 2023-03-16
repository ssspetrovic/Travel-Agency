using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface IActiveTourRepository
    {
        void Add(ActiveTourModel activeTourModel);
        void Edit(ActiveTourModel activeTourModel);
        void Remove(int id);
        ActiveTourModel GetById(int id);
        DataTable GetByAll(DataTable dt);
    }
}
