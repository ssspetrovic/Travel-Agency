using LiveCharts;

namespace TravelAgency.Model
{
    public class CurrentRequestTabs
    {
        public string Title { get; set; } = null!;
        public string DataType { get; set; } = null!;
        public string DataContent { get; set; } = null!;
        public int NumberOfRequests { get; set; }
        public SeriesCollection BarData { get; set; } = null!;
        public string[] BarLabels { get; set; } = null!;
    }
}
