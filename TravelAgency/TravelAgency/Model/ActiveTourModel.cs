using System.Collections.Generic;

namespace TravelAgency.Model
{
    public class ActiveTourModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, bool> KeyPoints { get; set; }
        public List<TouristModel> Tourists { get; set; }

        public ActiveTourModel(string name, Dictionary<int, bool> keyPoints, List<TouristModel> tourists)
        {
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
        }

        public ActiveTourModel(int id, string name, Dictionary<int, bool> keyPoints, List<TouristModel> tourists)
        {
            Id = id;
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
        }

    }
}
