using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.DTO
{
    public class AccommodationStatDTO
    {
        public string Period { get; set; } = string.Empty;
        public int ReservationCount { get; set; }
        public int CanceledReservations { get; set; }
        public int DelayedReseravations { get; set;}
        public int RenovationSuggestions { get; set;}

        public AccommodationStatDTO() { }

        public AccommodationStatDTO(string period, int reservationCount, int canceledReservations, int delayedReseravations, int renovationSuggestions)
        {
            Period = period;
            ReservationCount = reservationCount;
            CanceledReservations = canceledReservations;
            DelayedReseravations = delayedReseravations;
            RenovationSuggestions = renovationSuggestions;
        }
    }
}
