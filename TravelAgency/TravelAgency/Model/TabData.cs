using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveCharts;

namespace TravelAgency.Model
{

    public class TabData
    {
        public string? Title { get; set; }
        public ObservableCollection<FinishedTour>? Data { get; set; }
        public string? Name { get; set; }
        public List<Location>? KeyPoints { get; set; }
        public string[] BarLabels { get; set; }
        public SeriesCollection BarData { get; set; }
        public SeriesCollection PieChartData { get; set; }
    }
}
