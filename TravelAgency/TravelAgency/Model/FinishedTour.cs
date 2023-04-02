using System.Collections.Generic;

namespace TravelAgency.Model
{
    public class FinishedTour
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public List<Location?> KeyPoints { get; set; }
        public List<Tourist> Tourists { get; set; }

        public FinishedTour(string name, List<Location?> keyPoints, List<Tourist> tourists)
        {
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
        }

        public FinishedTour(int id, string name, List<Location?> keyPoints, List<Tourist> tourists)
        {
            Id = id;
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
        }
    }
}
