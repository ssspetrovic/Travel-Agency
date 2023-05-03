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
    }
}
