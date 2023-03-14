using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal interface ITourRepository
    {
        void Add(TourModel tourModel);
        void Edit(TourModel tourModel);
        void Remove(int id);
        TourModel GetById(int id);
        TourModel GetByName(string? name);
        IEnumerable<TourModel> GetByAll();
        Language FindLanguage(string language);
    }
}
