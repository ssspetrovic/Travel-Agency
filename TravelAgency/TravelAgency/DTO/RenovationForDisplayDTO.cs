using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.DTO
{
    public class RenovationForDisplayDTO
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public string AccName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public string StringStartDate { get; set; } =string.Empty;
        public string StringEndDate { get; set; } = string.Empty;

        public RenovationForDisplayDTO() { }

        public RenovationForDisplayDTO(int id, int accommodationId, DateTime startDate, DateTime endDate, int duration, string description, string accName)
        {
            Id = id;
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            Description = description;
            AccName = accName;
        }

        public RenovationForDisplayDTO(Renovation r, string accName)
        {
            Id = r.Id;
            AccommodationId = r.AccommodationId;
            StartDate = r.StartDate;
            EndDate= r.EndDate;
            Duration = r.Duration;
            Description = r.Description;
            AccName = accName;
            StringStartDate = r.StartDate.ToShortDateString();
            StringEndDate = r.EndDate.ToShortDateString();
        }
    }
}
