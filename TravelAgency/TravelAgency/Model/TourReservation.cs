using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal class TourReservation
    {
        public string Name { get; }
        public float Duration { get; }
        public LocationModel Location { get; }
        public Language Language { get; }
        public int MaxTourists { get; }

        public TourReservation(string name, float duration, LocationModel location, Language language, int maxTourists)
        {
            Name = name;
            Duration = duration;
            Location = location;
            Language = language;
            MaxTourists = maxTourists;
        }
    }
}
