using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class AccommodationActivity
    {
        public int AccommodationId { get; set; }
        public DateTime StartDate { get; set; }
        public int CanceledReservation { get; set; } = 0;
        public int DelayedReseravation { get; set; } = 0;
        public int RenovationSuggestion { get; set; } = 0;

        public AccommodationActivity() { }

        public AccommodationActivity(int accommodationId, DateTime startDate, int canceledReservation, int delayedReseravation, int renovationSuggestion)
        {
            AccommodationId = accommodationId;
            StartDate = startDate;
            CanceledReservation = canceledReservation;
            DelayedReseravation = delayedReseravation;
            RenovationSuggestion = renovationSuggestion;
        }
    }
}
