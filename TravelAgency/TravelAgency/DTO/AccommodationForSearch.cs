using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.DTO
{
    public class AccommodationForSearch
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public AccommodationType Type { get; set; }
        public int MaxGuest { get; set; }
        public int MinGuest { get; set; }
        public int ReservableDays { get; set; }
        public string PictureUrl { get; set; } = null!;
        public string isRenovated { get; set; } = string.Empty;
        public string isSuperOwner { get; set; } = string.Empty;

        public AccommodationForSearch() { }

        public AccommodationForSearch(AccommodationDTO accommodation, string isRenovated, string isSuperOwner)
        {
            Id = accommodation.Id;
            Name = accommodation.Name;
            Location = accommodation.Location;
            Type = accommodation.Type;
            MaxGuest = accommodation.MaxGuest;
            MinGuest = accommodation.MinGuest;
            ReservableDays = accommodation.ReservableDays;
            PictureUrl = accommodation.PictureUrl;
            this.isRenovated = isRenovated;
            this.isSuperOwner = isSuperOwner;
        }
    }
}
