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

        public bool Reserve(DateTime? startDate, DateTime? endDate, int Id) {
            ObservableCollection<Reservation> reservations = GetAllByAccommodationId(Id);

            DateTime convertedStartDate = Convert.ToDateTime(startDate);
            DateTime convertedEndDate = Convert.ToDateTime(endDate);

            foreach (Reservation reservation in reservations)
            {
                if((reservation.StartDate>startDate && reservation.EndDate<startDate) || (reservation.StartDate > endDate && reservation.EndDate < endDate))
                {
                    return false;
                }
            }
            var reservationRepository = new ReservationRepository();
            
            return true;
        }
    }
}
