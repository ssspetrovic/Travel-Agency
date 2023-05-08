using System;

namespace TravelAgency.Model
{
    internal class RegularTourRequest
    {
        public int Id { get; set; }
        public string TouristUsername { get; set; }
        public Location Location { get; set; }
        public Language Language { get; set; }
        public int GuestNumber { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Description { get; set; }

        public RegularTourRequest(string touristUsername, Location location, Language language, int guestNumber, DateTime startingDate, DateTime endingDate, string description)
        {
            TouristUsername = touristUsername;
            Location = location;
            Language = language;
            GuestNumber = guestNumber;
            StartingDate = startingDate;
            EndingDate = endingDate;
            Description = description;
        }

        public RegularTourRequest(int id, string touristUsername, Location location, Language language, int guestNumber, DateTime startingDate, DateTime endingDate, string description)
        {
            Id = id;
            TouristUsername = touristUsername;
            Location = location;
            Language = language;
            GuestNumber = guestNumber;
            StartingDate = startingDate;
            EndingDate = endingDate;
            Description = description;
        }
    }
}
