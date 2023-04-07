using System.Collections.Generic;

namespace TravelAgency.Model
{
    public class FinishedTour
    {
        public int Id { get; set; }
        public string Name { get; set;} = null!;
        public List<Location?> KeyPoints { get; set; } = null!;
        public List<Tourist> Tourists { get; set; } = null!;
        public string Date { get; set; } = null!;

        public FinishedTour() {}

        public FinishedTour(string name, List<Location?> keyPoints, List<Tourist> tourists, string date)
        {
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
            Date = date;
        }

        public FinishedTour(int id, string name, List<Location?> keyPoints, List<Tourist> tourists, string date)
        {
            Id = id;
            Name = name;
            KeyPoints = keyPoints;
            Tourists = tourists;
            Date = date;
        }
    }
}
