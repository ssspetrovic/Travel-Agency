using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class AccommodationService : RepositoryBase
    {
        private readonly AccommodationRepository accommodationRepository;

        public AccommodationService()
        {
            accommodationRepository = new AccommodationRepository();
        }

        public ObservableCollection<string> GetAccommodationNames()
        {
            ObservableCollection<string> names = new ObservableCollection<string>();
            ObservableCollection<AccommodationDTO> acc = accommodationRepository.GetAll();
            foreach(AccommodationDTO a in acc)
            {
                names.Add(a.Name);
            }
            return names;
        }
    }
}
