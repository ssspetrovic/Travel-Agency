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
            AccommodationStatDTO statDTO;
            for (int i = yearStart; i <= 2023; i++) 
            {
                string period = i.ToString();
                int reservationCount = reservationService.GetReservationCountByYear(i, accID);
                int canceledReservations = GetCanceledReservationByYear(i, accID);
                int delayedReseravations = GetDelayedReservationByYear(i, accID);
                int renovationSuggestions = GetRenovationSuggReservationByYear(i, accID);
                statDTO = new AccommodationStatDTO(period, reservationCount, canceledReservations, delayedReseravations, renovationSuggestions);
                accList.Add(statDTO);
            }

            return accList;
        }

        private int GetCanceledReservationByYear(int year, int accID)
        {
            var accommodationActivityRepository = new AccommodationActivityRepository();
            List<AccommodationActivity> accommodationActivities = accommodationActivityRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach(AccommodationActivity acc in accommodationActivities)
            {
                if (acc.StartDate.Year == year)
                    count += acc.CanceledReservation;
            }
            return count;
        }

        private int GetDelayedReservationByYear(int year, int accID)
        {
            var accommodationActivityRepository = new AccommodationActivityRepository();
            List<AccommodationActivity> accommodationActivities = accommodationActivityRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach (AccommodationActivity acc in accommodationActivities)
            {
                if (acc.StartDate.Year == year)
                    count += acc.DelayedReseravation;
            }
            return count;
        }

        private int GetRenovationSuggReservationByYear(int year, int accID)
        {
            var accommodationActivityRepository = new AccommodationActivityRepository();
            List<AccommodationActivity> accommodationActivities = accommodationActivityRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach (AccommodationActivity acc in accommodationActivities)
            {
                if (acc.StartDate.Year == year)
                    count += acc.RenovationSuggestion;
            }
            return count;
        }
    }
}
