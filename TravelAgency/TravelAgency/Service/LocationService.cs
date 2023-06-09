using System.Collections.Generic;
using System.Collections.ObjectModel;
using TravelAgency.Interface;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class LocationService : RepositoryBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = new LocationRepository();
        }

        public Location? GetById(int id)
        {
            return _locationRepository.GetById(id);
        }

        public Location? GetByCity(string city)
        {
            return _locationRepository.GetByCity(city);
        }

        public List<Location?> GetByAllCities(List<string> cities)
        {
            return _locationRepository.GetByAllCities(cities);
        }

        public ObservableCollection<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public string FindLocationIdByText(string text)
        {
            return _locationRepository.FindLocationIdByText(text);
        }

    }
}
