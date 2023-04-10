using System;

namespace TravelAgency.Model
{
    public class TourVoucher
    {
        public int Id { get; }
        public int TouristId { get; set; }
        public string TouristUsername { get; set; }
        public string? Description { get; set; }
        public DateTime ExpirationDate { get; set; }

        public TourVoucher(int touristId, string touristUsername, string description, DateTime expirationDate)
        {
            TouristId = touristId;
            TouristUsername = touristUsername;
            Description = description;
            ExpirationDate = expirationDate;
        }

        public TourVoucher(int id, int touristId, string touristUsername, string description, DateTime expirationDate)
        {
            Id = id;
            TouristUsername = touristUsername;
            TouristId = touristId;
            Description = description;
            ExpirationDate = expirationDate;
        }
    }
}
