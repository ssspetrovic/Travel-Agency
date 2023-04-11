using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class DelayRequest
    {
        public int AccommodationId { get; set; }
        public int UserId { get; set; }
        public DateTime OldStartDate { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime OldEndDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public DelayRequest() { }

        public DelayRequest(int accommodationId, int userId, DateTime oldStartDate, DateTime newStartDate, DateTime oldEndDate, DateTime newEndDate)
        {
            AccommodationId = accommodationId;
            UserId = userId;
            OldStartDate = oldStartDate;
            NewStartDate = newStartDate;
            OldEndDate = oldEndDate;
            NewEndDate = newEndDate;
        }
    }
}
