using System;
using System.Collections.Generic;
using System.Drawing;

namespace TravelAgency.Model
{

    public enum Language
    {
        English,
        Spanish,
        French,
        Italian,
        Serbian,
        Portuguese,
        German,
        Chinese,
        Japanese,
        Indian
    }
    public class TourModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LocationModel Location { get; set; }
        public string? Description { get; set; }
        public Language Language { get; set; }
        public int MaxGuests { get; set; }
        public List<LocationModel> KeyPoints { get; set; }
        public string Date { get; set; }
        public float Duration { get; set; }
        public string Images { get; set; }

        public TourModel(string name, LocationModel location, string? description, Language language, int maxGuests, List<LocationModel> keyPoints, string date, float duration, string images)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoints = keyPoints;
            Date = date;
            Duration = duration;
            Images = images;
        }
    }
}
