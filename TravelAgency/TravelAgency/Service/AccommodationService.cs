﻿using System;
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
    }
}