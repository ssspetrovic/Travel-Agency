using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class Renovation
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = string.Empty;

        public Renovation() { }

        public Renovation(int id, int accommodationId, DateTime startDate, DateTime endDate, int duration, string description)
        {
            Id = id;
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            Description = description;
        }

        public Renovation(int accommodationId, DateTime startDate, DateTime endDate, int duration, string description)
        {
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            Description = description;
        }
    }
}
