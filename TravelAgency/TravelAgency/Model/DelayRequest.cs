using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class DelayRequest
    {
        public int AccommodationId;
        public int UserId;
        public DateTime OldStartDate;
        public DateTime NewStartDate;
        public DateTime OldEndDate;
        public DateTime NewEndDate;
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
