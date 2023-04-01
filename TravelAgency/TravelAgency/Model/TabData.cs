using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TravelAgency.Model
{

    public class TabData
    {
        public string? Title { get; set; }
        public ObservableCollection<Tour>? Data { get; set; }
        public string? Name { get; set; }
        public List<Location>? KeyPoints { get; set; }
        public List<string>? BarNumber { get; set; }
        public List<double>? Bars { get; set; }
        public List<double>? PieChartData => Bars;
    }
}
