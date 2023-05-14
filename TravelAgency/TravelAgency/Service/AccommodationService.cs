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
        private readonly ReservationService reservationService;

        public AccommodationService()
        {
            accommodationRepository = new AccommodationRepository();
            reservationService = new ReservationService();
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

        public ObservableCollection<AccommodationStatDTO> GetAccommodationStatByYear(int accID)
        {
            ObservableCollection<AccommodationStatDTO> accList = new ObservableCollection<AccommodationStatDTO>();

            int yearStart = 2020;
            AccommodationStatDTO statDTO = new AccommodationStatDTO();
            for (int i = yearStart; i <= 2023; i++) 
            {
                statDTO.Period = i.ToString();
                statDTO.ReservationCount = reservationService.GetReservationCountByYear(i, accID);


                accList.Add(statDTO);
            }

            return accList;
        }
    }
}
