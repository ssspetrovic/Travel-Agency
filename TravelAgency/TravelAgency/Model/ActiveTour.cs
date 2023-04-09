using System.Collections.Generic;

namespace TravelAgency.Model
{
    public class ActiveTour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, bool> KeyPoints { get; set; }
        public List<Tourist> Tourists { get; set; }
        public string CurrentKeyPoint { get; set; }

        public ActiveTour(string name, Dictionary<int, bool> keyPoints, List<Tourist> tourists, string currentKeyPoint)
        {
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
            CurrentKeyPoint = currentKeyPoint;
        }

        public ActiveTour(int id, string name, Dictionary<int, bool> keyPoints, List<Tourist> tourists, string currentKeyPoint)
        {
            Id = id;
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
            CurrentKeyPoint = currentKeyPoint;
        }

    }
}
