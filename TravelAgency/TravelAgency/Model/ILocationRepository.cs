using System.Collections.Generic;

namespace TravelAgency.Model
{
    internal interface ILocationRepository
    {
        void Add(LocationModel locationModel);
        void Edit(LocationModel locationModel);
        void Remove(int id);
        UserModel GetById(int id);
        LocationModel? GetByCity(string city);
        IEnumerable<UserModel> GetByAll();
    }
}
