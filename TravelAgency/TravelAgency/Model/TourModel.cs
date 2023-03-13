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
        public int Name { get; set; }
        public LocationModel Location { get; set; }
        public string? Description { get; set; }
        public Language Language { get; set; }
        public int MaxGuests { get; set; }
        public List<LocationModel> KeyPoints { get; set; }
        public List<DateTime>? Birthdate { get; set; }
        public float Duration { get; set; }
        public List<Image> Images { get; set; }

        public TourModel(int id, int name, LocationModel location, string? description, Language language, int maxGuests, List<LocationModel> keyPoints, List<DateTime>? birthdate, float duration, List<Image> images)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoints = keyPoints;
            Birthdate = birthdate;
            Duration = duration;
            Images = images;
        }
    }
}
