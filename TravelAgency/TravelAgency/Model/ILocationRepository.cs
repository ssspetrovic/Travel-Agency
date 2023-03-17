using System.Collections.Generic;

namespace TravelAgency.Model
{
    internal interface ILocationRepository
    {
        void Add(LocationModel locationModel);
        void Edit(LocationModel locationModel);
        void Remove(int id);
        LocationModel? GetById(int id);
        LocationModel? GetByCity(string city);
        IEnumerable<LocationModel> GetByAll();
        List<LocationModel?> GetByAllCities(List<string> cities);
    }
}
