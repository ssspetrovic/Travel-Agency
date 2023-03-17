using System;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    public class TouristRepository : RepositoryBase, ITouristRepository
    {
        public void Add(TouristModel tourModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(TouristModel tourModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public TouristModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TouristModel GetByName(string? name)
        {
            throw new NotImplementedException();
        }

        public TouristModel GetByTour(TourModel tour)
        {
            throw new NotImplementedException();
        }
    }
}
