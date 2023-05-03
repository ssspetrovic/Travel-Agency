using System;

namespace TravelAgency.Model
{
    public class TourVoucher
    {
        public enum VoucherStatus
        {
            Valid,
            Expired,
            Used
        }

        public int Id { get; }
        public int TouristId { get; set; }
        public string TouristUsername { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public VoucherStatus Status { get; set; }

        public TourVoucher(int touristId, string touristUsername, string description, DateTime expirationDate, VoucherStatus status)
        {
            TouristId = touristId;
            TouristUsername = touristUsername;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
        }

        public TourVoucher(int id, int touristId, string touristUsername, string description, DateTime expirationDate, VoucherStatus status)
        {
            Id = id;
            TouristUsername = touristUsername;
            TouristId = touristId;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
        }
    }
}
