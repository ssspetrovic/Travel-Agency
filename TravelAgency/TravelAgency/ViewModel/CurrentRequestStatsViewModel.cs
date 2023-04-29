using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class CurrentRequestStatsViewModel : GuideViewModel
    {
        private readonly ObservableCollection<RequestTour> _tabAllData;
        private readonly ObservableCollection<RequestTour> _tab2023Data;
        private readonly ObservableCollection<RequestTour> _tab2022Data;
        private readonly RequestTourService _requestTourService;

        public CurrentRequestStatsViewModel()
        {
            _requestTourService = new RequestTourService();
            var dataType = DataType.Split(":")[0];
            _tabAllData = _requestTourService.GetAllTimeRequestedTour(dataType);
            _tab2023Data = _requestTourService.GetMostRequestedToursByYear(dataType, "2023");
            _tab2022Data = _requestTourService.GetMostRequestedToursByYear(dataType, "2023");
        }

        public static string DataType { get; set; } = null!;

        public ObservableCollection<CurrentRequestTabs> Tabs =>
            new()
            {
                new CurrentRequestTabs
                {
                    Title = "Overall", 
                    DataType = DataType.Split(":")[0], 
                    DataContent = DataType.Split(":")[1], 
                    NumberOfRequests = _tabAllData.Count,
                    BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Primer",
                            Values = _requestTourService.GetComparisons(_tabAllData, DataType.Split(":")[0])
                        }
                    },
                    BarLabels = new[] {"10", "3", "5", "3"}

                },
                new CurrentRequestTabs
                { 
                    Title = "2023",
                    DataType = DataType.Split(":")[0],
                    DataContent = DataType.Split(":")[1],
                    NumberOfRequests = _tab2023Data.Count,
                    BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Primer",
                            Values = new ChartValues<double> {10, 3, 5, 3}
                        }
                    },
                    BarLabels = new[] {"10", "3", "5", "3"}
                },
                new CurrentRequestTabs 
                { 
                    Title = "2022",
                    DataType = DataType.Split(":")[0],
                    DataContent = DataType.Split(":")[1],
                    NumberOfRequests = _tab2022Data.Count,
                    BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Primer",
                            Values = new ChartValues<double> {10, 3, 5, 3}
                        }
                    },
                    BarLabels = new[] {"10", "3", "5", "3"}
                }
            };
    }
}
