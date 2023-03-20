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
        public int LocationId { get; set; }
        public AccommodationType Type { get; set; }
        public int MinReservationDays { get; set; }
        public int MaxReservationDays { get; set; }
        public string Adress { get; set; } 
        public int ReservableDays { get; set; }
        public string Images { get; set; }  
        public string Description { get; set; }

        public Accommodation()
        {
            
        }
        public Accommodation(int id, string name, int locationId, AccommodationType type, int minReservationDays, int maxReservationDays, string adress, int reservableDays, string images, string description)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Type = type;
            MinReservationDays = minReservationDays;
            MaxReservationDays = maxReservationDays;
            Adress = adress;
            ReservableDays = reservableDays;
            Images = images;
            Description = description;
        }
    }
}
