using System.Collections.Generic;
using System.Collections.ObjectModel;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    public interface ILocationRepository
    {
        Location? GetById(int id);
        Location? GetByCity(string city);
        List<Location?> GetByAllCities(List<string> cities);
        ObservableCollection<Location> GetAll();
        string FindLocationIdByText(string text);
    }

}
