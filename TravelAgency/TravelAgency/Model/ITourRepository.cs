using System.Collections.Generic;

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
