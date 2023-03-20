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
        public string TourName { get; set; }
        public int GuestNumber { get; set; }
        public string Username { get; set; }
        public string TouristName{ get; set; }

        public TourReservation(int tourId, string tourName, int guestNumber, string username, string touristName)
        {
            TourId = tourId;
            TourName = tourName;
            GuestNumber = guestNumber;
            Username = username;
            TouristName = touristName;
        }
    }
}
