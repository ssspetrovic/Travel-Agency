using System;

namespace TravelAgency.Model
{ 
    
    public class TourVoucher
    {
        public int Id { get; }
        public int TouristId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }

        public TourVoucher() {}

        public TourVoucher(int touristId, string description, DateTime expirationDate)
        {
            TouristId = touristId;
            Description = description;
            ExpirationDate = expirationDate;
        }

        public TourVoucher(int id, int touristId, string description, DateTime expirationDate)
        {
            Id = id;
            TouristId = touristId;
            Description = description;
            ExpirationDate = expirationDate;
        }
    }
}
