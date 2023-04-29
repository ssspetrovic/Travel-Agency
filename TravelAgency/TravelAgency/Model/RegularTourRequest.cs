using System;

namespace TravelAgency.Model
{
    internal class RegularTourRequest
    {
        public Location Location { get; set; }
        public Language Language { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Description { get; set; }

        public RegularTourRequest(Location location, Language language, DateTime startingDate, DateTime endingDate, string description)
        {
            Location = location;
            Language = language;
            StartingDate = startingDate;
            EndingDate = endingDate;
            Description = description;
        }
    }
}
