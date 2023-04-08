using System.Collections.Generic;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class LocationService : RepositoryBase
    {
        private readonly LocationRepository _locationRepository;

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
    }
}
