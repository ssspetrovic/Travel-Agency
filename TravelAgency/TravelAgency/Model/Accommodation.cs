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
        public int OwnerId { get; set; }
        public string Name { get; set; } = null!;
        public int LocationId { get; set; }
        public AccommodationType Type { get; set; }
        public int MinGuest { get; set; }
        public int MaxGuest { get; set; }
        public string Address { get; set; } 
        public int ReservableDays { get; set; }
        public string Images { get; set; }  
        public string Description { get; set; }

        public Accommodation()
        {
            
        }
        public Accommodation(int id, string name, int locationId, AccommodationType type, int minReservationDays, int maxReservationDays, string address, int reservableDays, string images, string description)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Type = type;
            MinGuest = minReservationDays;
            MaxGuest = maxReservationDays;
            Address = address;
            ReservableDays = reservableDays;
            Images = images;
            Description = description;
        }

        public Accommodation(string name, int locationId, AccommodationType type, int minReservationDays, int maxReservationDays, string address, int reservableDays, string images, string description)
        {
            Name = name;
            LocationId = locationId;
            Type = type;
            MinGuest = minReservationDays;
            MaxGuest = maxReservationDays;
            Address = address;
            ReservableDays = reservableDays;
            Images = images;
            Description = description;
        }

        public Accommodation(string name, int locationId, AccommodationType type, int minReservationDays, int maxReservationDays, string address, int reservableDays, string images, string description, int ownerId)
        {
            Name = name;
            LocationId = locationId;
            Type = type;
            MinGuest = minReservationDays;
            MaxGuest = maxReservationDays;
            Address = address;
            ReservableDays = reservableDays;
            Images = images;
            Description = description;
            OwnerId = ownerId;
        }
    }
}
