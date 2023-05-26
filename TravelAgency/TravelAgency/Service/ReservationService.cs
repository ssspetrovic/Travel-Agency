using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DTO;
using TravelAgency.Repository;
using TravelAgency.Model;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Windows;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace TravelAgency.Service
{
    public class ReservationService
    {
        ObservableCollection<Reservation> GetAll() {
            var reservationRepository = new ReservationRepository();
            return reservationRepository.GetAll();
        }
        ObservableCollection<Reservation> GetAllByAccommodationId(int Id)
        {
            var reservationRepository = new ReservationRepository();
            return reservationRepository.GetAllByAccommodationId(Id);
        }

        public bool IsReservationValid(DateTime? startDate, DateTime? endDate, int maxDays)
        {
            DateTime convertedStartDate = Convert.ToDateTime(startDate);
            DateTime convertedEndDate = Convert.ToDateTime(endDate);

            if (convertedEndDate>convertedStartDate || (convertedStartDate - convertedEndDate).TotalDays > maxDays) return false;

            return true;
        }

        public DateTime FindDate(DateTime? endDate, DateTime? startDate, AccommodationDTO accommodationDTO)
        {
            ObservableCollection<Reservation> reservations = GetAllByAccommodationId(accommodationDTO.Id);

            DateTime convertedStartDate = Convert.ToDateTime(startDate);
            DateTime convertedEndDate = Convert.ToDateTime(endDate);

            double daysAtAccommodation = (convertedEndDate - convertedStartDate).TotalDays;
            DateTime maxDate = reservations[0].EndDate;
            int i;

            for (i= 0; i<reservations.Count()-1; i++)
            {
                if(DateTime.Compare(maxDate, reservations[i + 1].EndDate) < 0)
                {
                    maxDate = reservations[i+1].EndDate;
                }
                
                if ((reservations[i].EndDate - reservations[i + 1].StartDate).TotalDays < daysAtAccommodation)
                {
                    return reservations[i].EndDate;
                }
            }

            return maxDate;

        }
        public void GradeAccommodation(int reservationId, string comment, string pictureUrl, int gradeClean, int gradeOwner)
        {
            var _repository = new ReservationRepository();
            _repository.UpdateAfterRatingAccommodation(reservationId, comment, pictureUrl, gradeClean, gradeOwner);
        }

        public ObservableCollection<ReservationDTO> GetAllDTO()
        {
            var _reservationRepository = new ReservationRepository();
            var reservations = _reservationRepository.GetAll();
            var reservationsDTO = new ObservableCollection<ReservationDTO>();
            foreach(Reservation reservation in reservations)
            {
                reservationsDTO.Add(new ReservationDTO(reservation));
            }
            return reservationsDTO;
        }



        public bool Reserve(DateTime? endDate, DateTime? startDate, AccommodationDTO accommodationDTO, int guestNumber) {
            ObservableCollection<Reservation> reservations = GetAllByAccommodationId(accommodationDTO.Id);

            DateTime convertedStartDate = Convert.ToDateTime(startDate);
            DateTime convertedEndDate = Convert.ToDateTime(endDate);



            foreach (Reservation reservation in reservations)
            {
                if((reservation.StartDate<= convertedStartDate && reservation.EndDate>= convertedStartDate) || (reservation.StartDate <= convertedEndDate && reservation.EndDate >= convertedEndDate))
                {
                    return false;
                }
            }
            var reservationRepository = new ReservationRepository();
            reservationRepository.AddAutoId(new Reservation(10, " ", convertedStartDate, convertedEndDate, -1, -1, CurrentUser.Id, accommodationDTO.Id, " ", -1, -1, " "));
            return true;
        }
        public AccommodationDTO GetAccommodation(Reservation reservation)
        {
            if (reservation == null)
            {
                return null;
            }
            AccommodationRepository _accommodationRepository = new();
            return _accommodationRepository.GetById(reservation.AccommodationId);

        }

        public AccommodationDTO GetAccommodation(int reservationId)
        {
 
            AccommodationRepository _accommodationRepository = new();
            return _accommodationRepository.GetById(reservationId);

        }

        public void RemoveById(int id)
        {
            ReservationRepository _reservationRepository = new();
            _reservationRepository.RemoveById(id);

        }
        public Location GetLocation(Reservation reservation) {
            if (reservation == null)
            {
                return null;
            }
            AccommodationRepository _accommodationRepository = new();

            return _accommodationRepository.GetById(reservation.AccommodationId).Location;

        }

        public List<FreeDatesDTO> GetFreeDates(int accId, int duration, DateTime startDate, DateTime endDate) 
        {
            var reservationRepository = new ReservationRepository();
            var renovationRepository = new RenovationRepository();
            ObservableCollection<Reservation> reservations = reservationRepository.GetAllByAccommodationId(accId);
            ObservableCollection<Renovation> renovations = renovationRepository.GetAllByAccommodationId(accId);

            List<FreeDatesDTO> listOfDates = GetListOfDates(startDate, endDate, duration);

            List<FreeDatesDTO> freeDates = GetFreeDates(listOfDates, reservations, renovations);

            return freeDates;
        }

        private List<FreeDatesDTO> GetListOfDates(DateTime startDate, DateTime endDate, int duration)
        {
            List<FreeDatesDTO> listOfDates = new List<FreeDatesDTO>();
            FreeDatesDTO f;

            DateTime startTemp = startDate;
            DateTime endTemp = startTemp.AddDays(duration - 1);
            if (endTemp > endDate)
            {
                return listOfDates;
            }

            f = new FreeDatesDTO(startTemp, endTemp);
            listOfDates.Add(f);

            while (endTemp != endDate)
            {
                startTemp = startTemp.AddDays(1);
                endTemp = endTemp.AddDays(1);
                f = new FreeDatesDTO(startTemp, endTemp);
                listOfDates.Add(f);
            }

            return listOfDates;
        }

        private List<FreeDatesDTO> GetFreeDates(List<FreeDatesDTO> listOfDates, ObservableCollection<Reservation> reservations, ObservableCollection<Renovation> renovations)
        {
            List<FreeDatesDTO> freeDates = new List<FreeDatesDTO>();
            foreach (FreeDatesDTO fdto in listOfDates)
            {
                bool available = true;
                foreach (Reservation r in reservations)
                {
                    /*Checking if date is in scope of reservation*/
                    if ((fdto.startDate >= r.StartDate && fdto.startDate <= r.EndDate) || (fdto.endDate >= r.StartDate && fdto.endDate <= r.EndDate))
                    {
                        available = false;
                        break;
                    }
                    if ((r.StartDate >= fdto.startDate && r.StartDate <= fdto.endDate) || (r.EndDate >= fdto.startDate && r.EndDate <= fdto.endDate))
                    {
                        available = false;
                        break;
                    }
                }
                foreach(Renovation r in renovations)
                {
                    /*Checking if date is in scope of other renovations*/
                    if ((fdto.startDate >= r.StartDate && fdto.startDate <= r.EndDate) || (fdto.endDate >= r.StartDate && fdto.endDate <= r.EndDate))
                    {
                        available = false;
                        break;
                    }
                    if ((r.StartDate >= fdto.startDate && r.StartDate <= fdto.endDate) || (r.EndDate >= fdto.startDate && r.EndDate <= fdto.endDate))
                    {
                        available = false;
                        break;
                    }
                }
                if (available)
                    freeDates.Add(fdto);
            }
            return freeDates;
        }






        public int GetReservationCountByYear(int year, int accID)
        {
            var reservationRepository = new ReservationRepository();
            ObservableCollection<Reservation> reservations = reservationRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach(Reservation reservation in reservations)
            {
                if(reservation.StartDate.Year == year)
                {
                    count++;
                }
            }
            return count;
        }

        public int GetReservationCountByMonth(int year, int month, int accID)
        {
            var reservationRepository = new ReservationRepository();
            ObservableCollection<Reservation> reservations = reservationRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach (Reservation reservation in reservations)
            {
                if (reservation.StartDate.Year == year)
                {
                    if(reservation.StartDate.Month == month)
                        count++;
                }
            }
            return count;
        }

        public int GetReservationDaysByYear(int year, int accID)
        {
            var reservationRepository = new ReservationRepository();
            ObservableCollection<Reservation> reservations = reservationRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach (Reservation reservation in reservations)
            {
                if (reservation.StartDate.Year == year || reservation.EndDate.Year == year)
                {
                    for(DateTime d = reservation.StartDate; d <= reservation.EndDate; d = d.AddDays(1))
                    {
                        if(d.Year == year)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public int GetReservationDaysByMonth(int year, int month, int accID)
        {
            var reservationRepository = new ReservationRepository();
            ObservableCollection<Reservation> reservations = reservationRepository.GetAllByAccommodationId(accID);
            int count = 0;
            foreach (Reservation reservation in reservations)
            {
                if (reservation.StartDate.Year == year || reservation.EndDate.Year == year)
                {
                    for (DateTime d = reservation.StartDate; d <= reservation.EndDate; d = d.AddDays(1))
                    {
                        if (d.Year == year && d.Month == month)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public bool isSuperOwner(int ownerId)
        {
            var reservationRepository = new ReservationRepository();
            int gradeCount = reservationRepository.CountGradesForOwner(CurrentUser.Id);
            double averageGrade = reservationRepository.AverageGradeForOwner(CurrentUser.Id);

            if (gradeCount >= 50 && averageGrade >= 9.5)
                return true;
            else
                return false;
        }

        public ObservableCollection<ReservationDisplayReviewsDTO> GetGuestsGradesToDisplay()
        {
            var reservationRepository = new ReservationRepository();
            var accommodationRepository = new AccommodationRepository();
            ObservableCollection<Reservation> reservations = reservationRepository.GetAllByOwnerId(CurrentUser.Id);
            ObservableCollection<ReservationDisplayReviewsDTO> resToDisplay = new ObservableCollection<ReservationDisplayReviewsDTO>();
            foreach(Reservation reservation in reservations)
            {
                ReservationDisplayReviewsDTO r = new ReservationDisplayReviewsDTO();
                r.Id = reservation.Id;
                r.StartDate = reservation.StartDate.ToShortDateString();
                r.EndDate = reservation.EndDate.ToShortDateString();
                r.AccommodationId = reservation.AccommodationId;
                r.GuestId = reservation.GuestId;
                r.GradeAccommodationClean = reservation.GradeAccommodationClean;
                r.GradeAccommodationOwner = reservation.GradeAccommodationOwner;
                r.AccommodationComment = reservation.AccommodationComment;
                AccommodationDTO a = accommodationRepository.GetById(reservation.AccommodationId);
                r.AccommodationName = a.Name;

                if (reservation.GradeGuestComplacent != -1 && reservation.GradeGuestClean != -1)
                {
                    if (reservation.GradeAccommodationClean != -1 && reservation.GradeAccommodationOwner != -1)
                    {
                        resToDisplay.Add(r);
                    }
                }
            }
            return resToDisplay;
        }

        public ObservableCollection<ReservationDisplayReviewsDTO> GetGuestsToGrade()
        {
            var reservationRepository = new ReservationRepository();
            var accommodationRepository = new AccommodationRepository();
            ObservableCollection<Reservation> reservations = reservationRepository.GetAllByOwnerId(CurrentUser.Id);
            ObservableCollection<ReservationDisplayReviewsDTO> resToDisplay = new ObservableCollection<ReservationDisplayReviewsDTO>();
            foreach (Reservation reservation in reservations)
            {
                ReservationDisplayReviewsDTO r = new ReservationDisplayReviewsDTO();
                r.Id = reservation.Id;
                r.StartDate = reservation.StartDate.ToShortDateString();
                r.EndDate = reservation.EndDate.ToShortDateString();
                r.AccommodationId = reservation.AccommodationId;
                r.GuestId = reservation.GuestId;
                r.GradeAccommodationClean = reservation.GradeAccommodationClean;
                r.GradeAccommodationOwner = reservation.GradeAccommodationOwner;
                r.AccommodationComment = reservation.AccommodationComment;
                AccommodationDTO a = accommodationRepository.GetById(reservation.AccommodationId);
                r.AccommodationName = a.Name;

                if (reservation.GradeGuestComplacent == -1 && reservation.GradeGuestClean == -1)
                {
                    double days = (DateTime.Now - reservation.EndDate).TotalDays;
                    days = 4; //DELETE THIS
                    if (days <= 5)
                    {
                        resToDisplay.Add(r);
                    }
                }
            }
            return resToDisplay;
        }
    }
}
