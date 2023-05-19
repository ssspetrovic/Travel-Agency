using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.View.Controls.Guide;

namespace TravelAgency.DTO
{
    public class DelayRequestDTO
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

        public string AccommodationName { get; set; } = string.Empty;
        public string GuestName { get; set; } = string.Empty;

        public DelayRequestDTO() { }

        public DelayRequestDTO(int reservationId, int accommodationId, int userId, DateTime oldStartDate, DateTime newStartDate, DateTime oldEndDate, DateTime newEndDate, RequestStatusType requestStatus, string rejectionReason, string accName, string guestName)
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
            AccommodationName = accName;
            GuestName = guestName;
        }

        public DelayRequestDTO(DelayRequest d, string accName, string guestName)
        {
            ReservationId = d.ReservationId;
            AccommodationId = d.AccommodationId;
            UserId = d.UserId;
            OldStartDate = d.OldStartDate;
            NewStartDate = d.NewStartDate;
            OldEndDate = d.OldEndDate;
            NewEndDate = d.NewEndDate;
            RequestStatus = d.RequestStatus;
            RejectionReason = d.RejectionReason;
            AccommodationName = accName;
            GuestName = guestName;
        }
    }
}
