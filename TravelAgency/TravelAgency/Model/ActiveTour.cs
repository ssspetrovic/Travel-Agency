using System.Collections.Generic;

namespace TravelAgency.Model
{
    public class ActiveTour : Tour
    {
        public Dictionary<int, bool> CheckedKeyPoints { get; set; }
        public List<Tourist> Tourists { get; set; }
        public string CurrentKeyPoint { get; set; }

        public ActiveTour(string name, Dictionary<int, bool> checkedKeyPoints, List<Tourist> tourists, string currentKeyPoint)
        {
            Name = name;
            CheckedKeyPoints = checkedKeyPoints;
            Tourists = tourists;
            CurrentKeyPoint = currentKeyPoint;
        }

        public ActiveTour(int id, string name, Dictionary<int, bool> checkedKeyPoints, List<Tourist> tourists, string currentKeyPoint)
        {
            Id = id;
            Name = name;
            CheckedKeyPoints = checkedKeyPoints;
            Tourists = tourists;
            CurrentKeyPoint = currentKeyPoint;
        }

    }
}
