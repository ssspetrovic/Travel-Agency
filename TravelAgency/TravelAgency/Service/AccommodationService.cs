using iTextSharp.text;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
                int renovationSuggestions = GetRenovationSuggestionByYear(i, accID);
                statDTO = new AccommodationStatDTO(period, reservationCount, canceledReservations, delayedReseravations, renovationSuggestions);
                accList.Add(statDTO);
            }

            return accList;
        }

        public ObservableCollection<AccommodationStatDTO> GetAccommodationStatByMonth(int accID, int year)
        {
            ObservableCollection<AccommodationStatDTO> accList = new ObservableCollection<AccommodationStatDTO>();

            int monthStart = 1;
            AccommodationStatDTO statDTO;
            for (int i = monthStart; i <= 12; i++)
            {
                string period = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                int reservationCount = reservationService.GetReservationCountByMonth(year, i, accID);
                int canceledReservations = GetCanceledReservationByMonth(year, i, accID);
                int delayedReseravations = GetDelayedReservationByMonth(year, i, accID);
                int renovationSuggestions = GetRenovationSuggestionByMonth(year, i, accID);
                statDTO = new AccommodationStatDTO(period, reservationCount, canceledReservations, delayedReseravations, renovationSuggestions);
                accList.Add(statDTO);
            }

            return accList;
        }

        public List<Paragraph> CreateDocumentData(int accID, int year)
        {
            var documentData = new List<Paragraph>();

            int monthStart = 1;
            for (int i = monthStart; i <= 12; i++)
            {
                int reservationCount = reservationService.GetReservationCountByMonth(year, i, accID);
                int canceledReservations = GetCanceledReservationByMonth(year, i, accID);
                int delayedReseravations = GetDelayedReservationByMonth(year, i, accID);
                int renovationSuggestions = GetRenovationSuggestionByMonth(year, i, accID);

                if(CurrentLanguageAndTheme.languageId == 0)
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    string period = culture.DateTimeFormat.GetMonthName(i);
                    var data1 = new Paragraph($"Period: {period}\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
                    documentData.Add(data1);
                    var data2 = new Paragraph($"  Reservation count: {reservationCount}\n  Canceled reservations: {canceledReservations}\n" +
                        $"  Delayed reservations: {delayedReseravations}\n  Renovation suggestions: {renovationSuggestions}", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                    documentData.Add(data2);
                }
                else
                {
                    CultureInfo culture = new CultureInfo("sr-Latn-RS");
                    string period = culture.DateTimeFormat.GetMonthName(i);
                    var data1 = new Paragraph($"Period: {period}\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
                    documentData.Add(data1);
                    var data2 = new Paragraph($"  Broj rezervaicja: {reservationCount}\n  Otkazane rezervacije: {canceledReservations}\n" +
                        $"  Pomerene rezervacije: {delayedReseravations}\n  Sugestije za renoviranje: {renovationSuggestions}", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                    documentData.Add(data2);
                }
            }

            return documentData;
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

        private int GetRenovationSuggestionByYear(int year, int accID)
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

        private int GetCanceledReservationByMonth(int year, int month, int accID)
        {
            var accommodationActivityRepository = new AccommodationActivityRepository();
            List<AccommodationActivity> accommodationActivities = accommodationActivityRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach (AccommodationActivity acc in accommodationActivities)
            {
                if (acc.StartDate.Year == year)
                {
                    if (acc.StartDate.Month == month)
                        count += acc.CanceledReservation;
                }
            }
            return count;
        }

        private int GetDelayedReservationByMonth(int year, int month, int accID)
        {
            var accommodationActivityRepository = new AccommodationActivityRepository();
            List<AccommodationActivity> accommodationActivities = accommodationActivityRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach (AccommodationActivity acc in accommodationActivities)
            {
                if (acc.StartDate.Year == year)
                {
                    if (acc.StartDate.Month == month)
                        count += acc.DelayedReseravation;
                }
            }
            return count;
        }

        private int GetRenovationSuggestionByMonth(int year, int month, int accID)
        {
            var accommodationActivityRepository = new AccommodationActivityRepository();
            List<AccommodationActivity> accommodationActivities = accommodationActivityRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach (AccommodationActivity acc in accommodationActivities)
            {
                if (acc.StartDate.Year == year)
                {
                    if (acc.StartDate.Month == month)
                        count += acc.RenovationSuggestion;
                }
            }
            return count;
        }

        public string GetMostBusyByYear(int accID)
        {
            int mostBusyYear = 2020;

            int countMax = 0;

            for (int i = 2020; i <= 2023; i++)
            {
                int temp = reservationService.GetReservationDaysByYear(i, accID);
                if(temp > countMax)
                {
                    countMax = temp;
                    mostBusyYear = i;
                }
            }

            return mostBusyYear.ToString();
        }

        public string GetMostBusyByMonth(int year, int accID)
        {
            string mostBusyMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(1);

            int countMax = 0;

            for (int i = 1; i <= 12; i++)
            {
                int temp = reservationService.GetReservationDaysByMonth(year, i, accID);
                if (temp > countMax)
                {
                    countMax = temp;
                    mostBusyMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                }
            }

            return mostBusyMonth;
        }

        public ObservableCollection<AccommodationForSearch> GetAllForSearchResult()
        {
            var accommodationRepository = new AccommodationRepository();
            var renovationService = new RenovationService();
            var reservationService = new ReservationService();
            ObservableCollection<AccommodationDTO> accommodations = accommodationRepository.GetAll();
            ObservableCollection<AccommodationForSearch> notSuperOwner = new ObservableCollection<AccommodationForSearch>();

            ObservableCollection<AccommodationForSearch> accList = new ObservableCollection<AccommodationForSearch>();  //return sortirana lista

            string isRenovated;
            string isSuperOwner;
            foreach (AccommodationDTO accommodation in accommodations)
            {
                if (renovationService.IsRenovated(accommodation.Id))
                    isRenovated = "Recently Renovated";
                else
                    isRenovated = "Not Recently Renovated";

                if (reservationService.isSuperOwner(CurrentUser.Id))
                {
                    isSuperOwner = "Super Owner";
                    AccommodationForSearch acc = new AccommodationForSearch(accommodation, isRenovated, isSuperOwner);
                    accList.Add(acc);  //Prvo dodajemo super vlasnike u listu
                }
                else
                {
                    isSuperOwner = "Regular Owner";
                    AccommodationForSearch acc = new AccommodationForSearch(accommodation, isRenovated, isSuperOwner);
                    notSuperOwner.Add(acc);
                }
            }

            //Dodajemo regularne vlasnike u finalnu listu
            foreach(AccommodationForSearch acc in notSuperOwner)
            {
                accList.Add(acc);
            }

            return accList;
        }

        public AccommodationDTO FindById(int id)
        {
            var _repository = new AccommodationRepository();
            var accommodation = _repository.GetById(id);
            return accommodation;
        }
    }
}
