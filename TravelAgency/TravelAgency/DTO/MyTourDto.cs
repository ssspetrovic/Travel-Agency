using System;
using TravelAgency.Model;

namespace TravelAgency.DTO
{
    internal class MyTourDto
    {
        public enum TourStatus
        {
            Inactive,
            Active,
            Requested,
            Attending,
            Finished,
            Rated
        }

        public int TourId { get; private set; }
        public string? TouristUsername { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public TourStatus Status { get; set; }
        public string KeyPoint { get; set; }

        public MyTourDto(int tourId, string? touristUsername, string name, Location location, DateTime date, TourStatus status, string keyPoint)
        {
            TourId = tourId;
            TouristUsername = touristUsername;
            Name = name;
            Location = location;
            Date = date;
            Status = status;
            KeyPoint = keyPoint;
        }
    }
}
