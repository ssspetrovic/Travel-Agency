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
        public Location Location { get; set; } = null!;
        public AccommodationType Type { get; set; }
        public int MaxGuest { get; set; }
        public int MinGuest { get; set; }
        public int ReservableDays { get; set; }
        public string PictureUrl { get; set; } = null!;

        public AccommodationDTO()
        {
        }

        public AccommodationDTO(Accommodation accommodation, Location location) 
        {
            Id = accommodation.Id;
            Name = accommodation.Name;
            Type = accommodation.Type;
            MaxGuest = accommodation.MaxGuest;
            MinGuest = accommodation.MinGuest;
            ReservableDays = accommodation.ReservableDays;
            Location = location;
            

        }

        public AccommodationDTO(int id, string name, Location location, AccommodationType type, int maxGuest, int minGuest, int reservableDays)
        {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            MaxGuest = maxGuest;
            MinGuest = minGuest;
            ReservableDays = reservableDays;
        }

        public AccommodationDTO(int id, string name, Location location, AccommodationType type, int maxGuest, int minGuest, int reservableDays, string pictureUrl)
        {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            MaxGuest = maxGuest;
            MinGuest = minGuest;
            ReservableDays = reservableDays;
            PictureUrl = pictureUrl;
        }
    }
}
