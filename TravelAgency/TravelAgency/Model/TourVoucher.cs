using System;

namespace TravelAgency.Model
{
    internal class TourVoucher
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }

        public TourVoucher(int id, int touristId, string description, DateTime expireDate)
        {
            Id = id;
            TouristId = touristId;
            Description = description;
            ExpireDate = expireDate;
        }
    }
}
