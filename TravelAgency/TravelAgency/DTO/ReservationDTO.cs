using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.DTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateOnly StartDate {  get; set; }
        public DateOnly EndDate { get; set; }
        public Location Location { get; set; }
        public string AccommodationName { get; set; }
        public int AccommodationId { get; set; }

        public ReservationDTO() {}
        public ReservationDTO (Reservation reservation)
        {
            var _reservationService = new ReservationService();
            Id = reservation.Id;
            StartDate = DateOnly.FromDateTime(reservation.StartDate);
            EndDate = DateOnly.FromDateTime(reservation.EndDate);
            Location = _reservationService.GetLocation(reservation);
            AccommodationName = _reservationService.GetAccommodation(reservation).Name;
            AccommodationId = reservation.AccommodationId;
        }
    }
}
