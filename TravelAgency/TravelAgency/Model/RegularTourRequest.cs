using System;

namespace TravelAgency.Model
{
    internal class RegularTourRequest
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public Location Location { get; set; }
        public Language Language { get; set; }
        public int GuestNumber { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Description { get; set; }

        public RegularTourRequest(int touristId, Location location, Language language, int guestNumber, DateTime startingDate, DateTime endingDate, string description)
        {
            TouristId = touristId;
            Location = location;
            Language = language;
            GuestNumber = guestNumber;
            StartingDate = startingDate;
            EndingDate = endingDate;
            Description = description;
        }

        public RegularTourRequest(int id, int touristId, Location location, Language language, int guestNumber, DateTime startingDate, DateTime endingDate, string description)
        {
            Id = id;
            TouristId = touristId;
            Location = location;
            Language = language;
            GuestNumber = guestNumber;
            StartingDate = startingDate;
            EndingDate = endingDate;
            Description = description;
        }
    }
}
