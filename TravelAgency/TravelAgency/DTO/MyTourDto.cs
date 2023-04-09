using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.DTO
{
    internal class MyTourDto
    {
        public enum TourStatus
        {
            Inactive,
            Active,
            Finished,
            Attending,
            Unrated,
            Rated
        }

        public string Name { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public TourStatus Status { get; set; }
        public string KeyPoint { get; set; }

        public MyTourDto(string name, Location location, DateTime date, TourStatus status, string keyPoint)
        {
            Name = name;
            Location = location;
            Date = date;
            Status = status;
            KeyPoint = keyPoint;
        }
    }
}
