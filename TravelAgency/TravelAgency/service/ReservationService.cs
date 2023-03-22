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

        public bool IsReservationValid(DateTime startDate, DateTime endDate, int maxDays)
        {
            if(endDate>startDate || (endDate-startDate).TotalDays < maxDays) return false;

            return true;
        }

        public bool Reserve() {
            ObservableCollection<Reservation> reservations = GetAll();
            return true;
        }
    }
}
