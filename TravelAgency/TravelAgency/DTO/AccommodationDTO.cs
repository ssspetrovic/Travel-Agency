using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.DTO
{
    public class AccommodationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public LocationModel Location { get; set; } = null!;
        public AccommodationType Type { get; set; }
        public int MaxReservationDays { get; set; }

        public AccommodationDTO()
        {
        }

        public AccommodationDTO(Accommodation accommodation, LocationModel location) 
        {
            Id = accommodation.Id;
            Name = accommodation.Name;
            Type = accommodation.Type;
            Location = location;
            MaxReservationDays = accommodation.MaxReservationDays;

        }

        public AccommodationDTO(int id, string name, LocationModel location, AccommodationType type, int maxReservationDays)
        {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            MaxReservationDays = maxReservationDays;
        }
    }
}
