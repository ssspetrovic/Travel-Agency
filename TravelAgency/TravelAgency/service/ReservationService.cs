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

        public bool Reserve(DateTime? endDate, DateTime? startDate, AccommodationDTO accommodationDTO) {
            ObservableCollection<Reservation> reservations = GetAllByAccommodationId(accommodationDTO.Id);

            DateTime convertedStartDate = Convert.ToDateTime(startDate);
            DateTime convertedEndDate = Convert.ToDateTime(endDate);

            foreach (Reservation reservation in reservations)
            {
                //Debug.WriteLine(reservation.EndDate.ToString() + " " + convertedEndDate.ToString());
                if((reservation.StartDate<= convertedStartDate && reservation.EndDate>= convertedStartDate) || (reservation.StartDate <= convertedEndDate && reservation.EndDate >= convertedEndDate))
                {
                    Debug.WriteLine("If");
                    return false;
                }
            }
            var reservationRepository = new ReservationRepository();
            reservationRepository.AddAutoId(new Reservation(10, " ", convertedStartDate, convertedEndDate, -1, -1, CurrentUser.Id, accommodationDTO.Id));
            return true;
        }
    }
}
