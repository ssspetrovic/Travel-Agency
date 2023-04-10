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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Propertys for grading the GUEST
        public string UserComment { get; set; } = string.Empty;
        public int GuestId { get; set; }
        public float GradeGuestComplacent { get; set; }
        public float GradeGuestClean { get; set; }

        // Propertys for grading the OWNER
        public int AccommodationId { get; set; }
        public string AccommodationComment { get; set; }
        public float GradeAccommodationClean { get; set; }
        public float GradeAccommodationOwner { get; set; }
        public string ReviewImagesURL { get; set; }



        public Reservation()
        {
        }

        public Reservation(int id, string userComment, DateTime startDate, DateTime endDate, float gradeGuestComplacent, 
            float gradeGuestClean, int guestId, int accommodationId, string accomodationComment, float gradeAccommodationClean,
            float gradeAccommodationOwner, string reviewImagesURL)
        {
            Id = id;
            UserComment = userComment;
            StartDate = startDate;
            EndDate = endDate;
            GradeGuestComplacent = gradeGuestComplacent;
            GradeGuestClean = gradeGuestClean;
            GuestId = guestId;
            AccommodationId = accommodationId;
            AccommodationComment = accomodationComment;
            GradeAccommodationClean = gradeAccommodationClean;
            GradeAccommodationOwner = gradeAccommodationOwner;
            ReviewImagesURL = reviewImagesURL;
        }

        public Reservation(int id, string userComment, DateTime startDate, DateTime endDate, float gradeGuestComplacent, float gradeGuestClean, int guestId, int accommodationId)
        {
            Id = id;
            UserComment = userComment;
            StartDate = startDate;
            EndDate = endDate;
            GradeGuestComplacent = gradeGuestComplacent;
            GradeGuestClean = gradeGuestClean;
            GuestId = guestId;
            AccommodationId = accommodationId;
        }
    }
}
