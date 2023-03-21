using System.Collections.Generic;

namespace TravelAgency.Model
{
    internal interface ITouristRepository
    {
        void Add(TouristModel tourModel);
        void Edit(TouristModel tourModel);
        void Remove(int id);
        TouristModel GetById(int id);
        TouristModel GetByUsername(string? username);
        List<TouristModel> GetByTour(TourModel tour);
        void CheckTourist(string username);
        void RemoveTour(int id);
    }
}
