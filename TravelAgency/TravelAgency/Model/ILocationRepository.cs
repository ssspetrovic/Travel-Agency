using System.Collections.Generic;
using System.Data;

namespace TravelAgency.Model
{
    internal interface ILocationRepository
    {
        void Add(Location location);
        void Edit(Location location);
        void Remove(int id);
        Location? GetById(int id);
        Location? GetByCity(string city);
        DataTable GetAll(DataTable dt);
        List<Location?> GetByAllCities(List<string> cities);
    }
}
