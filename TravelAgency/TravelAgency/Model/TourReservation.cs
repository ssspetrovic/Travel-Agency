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
        public string Username { get; set; }
        public string DisplayName{ get; set; }

        public TourReservation(int id, int tourId, string username, string displayName)
        {
            Id = id;
            TourId = tourId;
            Username = username;
            DisplayName = displayName;
        }
    }
}
