using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.DTO
{
    public class ReservationDisplayReviewsDTO
    {
        public int Id { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public int AccommodationId { get; set; }
        public string AccommodationName { get; set; } = string.Empty;
        public string AccommodationComment { get; set; } = string.Empty;
        public float GradeAccommodationClean { get; set; }
        public float GradeAccommodationOwner { get; set; }
        public int GuestId { get; set; }

        public ReservationDisplayReviewsDTO() { }
        public ReservationDisplayReviewsDTO(int id, string AccommodationComment, string startDate, string endDate, float GradeAccommodationClean, float GradeAccommodationOwner, int guestId, int accommodationId, string accommodationName)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.GradeAccommodationClean = GradeAccommodationClean;
            this.GradeAccommodationOwner = GradeAccommodationOwner;
            this.AccommodationId = accommodationId;
            this.AccommodationName = accommodationName;
            this.GuestId = guestId;
        }
    }
}
