using System.Collections.Generic;

namespace TravelAgency.Model
{
    internal interface ITouristRepository
    {
        void Add(Tourist tour);
        void Edit(Tourist tour);
        void Remove(int id);
        Tourist GetById(int id);
        Tourist GetByUsername(string? username);
        List<Tourist> GetByTour(Tour tour);
        void CheckTourist(string username);
        void RemoveTour(int id);
    }
}
