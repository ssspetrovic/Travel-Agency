using System.Collections.Generic;

namespace TravelAgency.Model
{
    public class ActiveTourModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private SortedDictionary<int, bool> KeyPoints { get; set; }
        private SortedDictionary<string, bool> Tourists { get; set; }
        private SortedDictionary<string, int> TouristAppearance { get; set; }

        public ActiveTourModel(int id, string name, SortedDictionary<int, bool> keyPoints, SortedDictionary<string, bool> tourists, SortedDictionary<string, int> touristAppearance)
        {
            Id = id;
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
            TouristAppearance = touristAppearance;
        }

    }
}
