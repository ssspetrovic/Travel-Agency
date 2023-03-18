using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public enum AccommodationType
    {
        Apartment = 1,
        House,
        Cottage
    }
    public class Accommodation
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public LocationModel Location { get; set; } = null!;
        public AccommodationType Type { get; set; }
        public int minReservationDays { get; set; }
        public int daysBeforeCancelation { get; set; }

        public Accommodation()
        {
            
        }

        public Accommodation(string name, LocationModel location, AccommodationType type, int minReservationDays, int daysBeforeCancelation)
        {
            this.Name = name;
            this.Location = location;
            this.Type = type;
            this.minReservationDays = minReservationDays;
            this.daysBeforeCancelation = daysBeforeCancelation;
        }

    }
}
