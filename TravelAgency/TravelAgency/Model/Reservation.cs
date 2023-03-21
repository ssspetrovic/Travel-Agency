using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float GradeComplaisent { get; set; }
        public float GradeClean { get; set; }
        public int GuestId { get; set; }
        public int AccommodationId { get; set; }

        public Reservation()
        {
        }

        public Reservation(int id, string comment, DateTime startDate, DateTime endDate, float gradeComplaisent, float gradeClean, int guestId, int accommodationId)
        {
            Id = id;
            Comment = comment;
            StartDate = startDate;
            EndDate = endDate;
            GradeComplaisent = gradeComplaisent;
            GradeClean = gradeClean;
            GuestId = guestId;
            AccommodationId = accommodationId;
        }
    }
}
