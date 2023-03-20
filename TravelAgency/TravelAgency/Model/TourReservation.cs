using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class TourReservation
    {
        public int Id { get; }
        public int TourId { get; set; }
        public int GuestNumber { get; set; }
        public string Username { get; set; }
        public string DisplayName{ get; set; }

        public TourReservation(int tourId, int guestNumber, string username, string displayName)
        {
            TourId = tourId;
            GuestNumber = guestNumber;
            Username = username;
            DisplayName = displayName;
        }
    }
}
