using System.Collections.Generic;
using System.Text;

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
        Indian,
        Unknown
    }
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public string? Description { get; set; }
        public Language Language { get; set; }
        public int MaxGuests { get; set; }
        public List<Location?> KeyPoints { get; set; } = null!;
        public string Date { get; set; } = null!;
        public float Duration { get; set; }
        public string Photos { get; set; } = null!;
        public List<Tourist> Tourists { get; set; } = null!;
        public string GuideName { get; set; } = null!;

        public Tour()
        {

        }

        public Tour(string name, List<Location?> keyPoints, List<Tourist> tourists, string date)
        {
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
            Date = date;
        }

        public Tour(int id, string name, List<Location?> keyPoints, List<Tourist> tourists, string date)
        {
            Id = id;
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
            Date = date;
        }

        public Tour(string name, Location location, string? description, Language language, int maxGuests, List<Location?> keyPoints, string date, float duration, string photos)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoints = keyPoints;
            Date = date;
            Duration = duration;
            Photos = photos;
        }

        public Tour(int id, string name, Location location, string? description, Language language, int maxGuests, List<Location?> keyPoints, string date, float duration, string photos)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoints = keyPoints;
            Date = date;
            Duration = duration;
            Photos = photos;
        }

        public Tour(int id, string name, Location location, string? description, Language language, int maxGuests, List<Location?> keyPoints, string date, float duration, string photos, string guideName)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoints = keyPoints;
            Date = date;
            Duration = duration;
            Photos = photos;
            GuideName = guideName;
        }

        public Tour(int id, string name, Location location, string? description, Language language, int maxGuests, List<Location?> keyPoints, string date, float duration, string photos, List<Tourist> tourists)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            KeyPoints = keyPoints;
            Date = date;
            Duration = duration;
            Photos = photos;
            Tourists = tourists;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(
                $"{Id,-3} {Name,-12} {Location,-30} {Description,-20} {Language.ToString(),-20} {MaxGuests,-5} {Date,-25} {Duration,-5}");
            return sb.ToString();
        }
    }
}
