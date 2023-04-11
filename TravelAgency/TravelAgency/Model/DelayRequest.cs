using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public enum RequestStatusType
    {
        Processing = 1,
        Accepted,
        Rejected
    }
    public class DelayRequest
    {
        public int ReservationId { get; set; }
        public int AccommodationId { get; set; }
        public int UserId { get; set; }
        public DateTime OldStartDate { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime OldEndDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public RequestStatusType RequestStatus { get; set; }
        public string RejectionReason { get; set; }

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

        public DelayRequest(int reservationId, int accommodationId, int userId, DateTime oldStartDate, DateTime newStartDate, DateTime oldEndDate, DateTime newEndDate, RequestStatusType requestStatus, string rejectionReason)
        {
            ReservationId = reservationId;
            AccommodationId = accommodationId;
            UserId = userId;
            OldStartDate = oldStartDate;
            NewStartDate = newStartDate;
            OldEndDate = oldEndDate;
            NewEndDate = newEndDate;
            RequestStatus = requestStatus;
            RejectionReason = rejectionReason; 
        }
    }
}
