using System.Collections.Generic;
using System.Data;

namespace TravelAgency.Model
{
    internal interface ILocationRepository
    {
        void Add(LocationModel locationModel);
        void Edit(LocationModel locationModel);
        void Remove(int id);
        LocationModel? GetById(int id);
        LocationModel? GetByCity(string city);
        DataTable GetByAll(DataTable dt);
        List<LocationModel?> GetByAllCities(List<string> cities);
    }
}
