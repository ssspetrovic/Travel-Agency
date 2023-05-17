using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Controls.Guide;
using TravelAgency.View;
using System.Windows.Input;
using System;
using System.Globalization;

namespace TravelAgency.ViewModel
{
    public class CurrentRequestStatsViewModel : HomePageViewModel
    {
        private readonly ObservableCollection<RequestTour> _tabAllData;
        private readonly ObservableCollection<RequestTour> _tab2023Data;
        private readonly ObservableCollection<RequestTour> _tab2022Data;
        private readonly RequestTourService _requestTourService;
        private int _tabsIndex;

        public CurrentRequestStatsViewModel()
        {
            _requestTourService = new RequestTourService();
            _tabAllData = _requestTourService.GetAllTimeRequestedTour(DataType);
            _tab2023Data = _requestTourService.GetMostRequestedToursByYear(DataType, "2023");
            _tab2022Data = _requestTourService.GetMostRequestedToursByYear(DataType, "2022");
            NavCommand = new MyICommand(OnNav);
            TabTourStats = new MyICommand(TabPressed);
            SelectStatsByMonth = new MyICommand<string>(ByMonth);
        }

        public MyICommand NavCommand { get; private set; }

        public MyICommand TabTourStats { get; private set; }

        public MyICommand<string> SelectStatsByMonth { get; private set; }

        public string LoadCurrentUserData => "Welcome " + CurrentUser.DisplayName;

        public void OnNav()
        {
            var window = Application.Current.Windows.OfType<CurrentRequestStats>().FirstOrDefault();
            var mainWindow = new Guide();
            if (mainWindow.DataContext is not GuideViewModel guideViewModel) return;
            guideViewModel.CurrentViewModel = new RequestStatsViewModel();
            mainWindow.Title = "Request Stats";
            mainWindow.Show();
            window!.Close();
        }

        public void ByMonth(string month)
        {
            if (!Enum.TryParse(month, out Key key)) return;
            if (TabsIndex == 0) return;
            var monthNumber = key - Key.F1 + 1;

            if (month == "October")
                monthNumber = 10;

            var tourList = _requestTourService.GetRequestsByDate("", monthNumber, Tabs[TabsIndex].Title, DataType);
            var numberOfRequests = 0.0;

            if (tourList.Count > 0)
            {
                var minStartDate = tourList.Min(tour => DateTime.Parse(tour.DateRange.Split(" - ")[0]));
                var maxEndDate = tourList.Max(tour => DateTime.Parse(tour.DateRange.Split(" - ")[1]));

                if (minStartDate.Month > monthNumber || maxEndDate.Month < monthNumber)
                    minStartDate = maxEndDate;
                if (minStartDate.Month != monthNumber)
                    minStartDate = new DateTime(minStartDate.Year, monthNumber - 1, DateTime.DaysInMonth(minStartDate.Year, monthNumber - 1));
                if (maxEndDate.Month != monthNumber)
                    maxEndDate = new DateTime(maxEndDate.Year, monthNumber, DateTime.DaysInMonth(maxEndDate.Year, monthNumber));

                numberOfRequests = (maxEndDate - minStartDate).TotalDays;
            }
               
            var showMonthlyRequestStatsViewModel = new ShowMonthlyRequestStatsViewModel()
            {
                NumberOfRequests = numberOfRequests.ToString(CultureInfo.CurrentCulture),

                CurrentMonth = new DateTimeFormatInfo().GetMonthName(monthNumber) + ", " + Tabs[TabsIndex].Title
            };
            var showMonthlyRequestStats = new ShowMonthlyRequestStats(showMonthlyRequestStatsViewModel);
            showMonthlyRequestStats.Show();
        }

        public void TabPressed()
        {
            if (TabsIndex < 2)
                TabsIndex++;
            else
                TabsIndex = 0;
        }

        public int TabsIndex
        {
            get => _tabsIndex;
            set
            {
                _tabsIndex = value;
                OnPropertyChanged();
            }
        }

        public static string DataType { get; set; } = null!;

        public ObservableCollection<RequestsData> Tabs =>
            new()
            {
                new RequestsData
                {
                    Title = "Overall", 
                    DataType = DataType.Split(":")[0], 
                    DataContent = DataType.Split(":")[1],
                    NumberOfRequests = "Number of Requests: " + _tabAllData.Sum(tour =>
                    {
                        var dates = tour.DateRange.Split(" - ");
                        var startDate = DateTime.Parse(dates[0]);
                        var endDate = DateTime.Parse(dates[1]);
                        if (startDate.Year > 2023)
                            return 0;
                        if (endDate.Year > 2023)
                            return (new DateTime(2023, 12, 31) - startDate).TotalDays;
                        return (endDate - startDate).TotalDays;
                    }),
                    BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Comparison of our current " + DataType.Split(":")[0] + " with others.",
                            Values = _requestTourService.GetComparisons(_tabAllData, DataType.Split(":")[0], DataType.Split(":")[1])
                        }
                    },
                    BarLabels = _requestTourService.GetComparisonLabels(_tabAllData, DataType.Split(":")[0], DataType.Split(":")[1])

                },
                new RequestsData
                { 
                    Title = "2023",
                    DataType = DataType.Split(":")[0],
                    DataContent = DataType.Split(":")[1],
                    NumberOfRequests = "Number of Requests: " + _tab2023Data.Sum(tour =>
                    {
                        var dates = tour.DateRange.Split(" - ");
                        var startDate = DateTime.Parse(dates[0]);
                        var endDate = DateTime.Parse(dates[1]);
                        if (startDate.Year > 2023)
                            return 0;
                        if (endDate.Year > 2023)
                            return (new DateTime(2023, 12, 31) - startDate).TotalDays;
                        return (endDate - startDate).TotalDays;
                    }),
                    BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Comparison of our current " + DataType.Split(":")[0] + " with others.",
                            Values = _requestTourService.GetComparisons(_tab2023Data, DataType.Split(":")[0], DataType.Split(":")[1])
                        }
                    },
                    BarLabels = _requestTourService.GetComparisonLabels(_tab2023Data, DataType.Split(":")[0], DataType.Split(":")[1])
                },
                new RequestsData 
                { 
                    Title = "2022",
                    DataType = DataType.Split(":")[0],
                    DataContent = DataType.Split(":")[1],
                    NumberOfRequests = "Number of Requests: " + _tab2022Data.Sum(tour =>
                    {
                        var dates = tour.DateRange.Split(" - ");
                        var startDate = DateTime.Parse(dates[0]);
                        var endDate = DateTime.Parse(dates[1]);
                        if (startDate.Year > 2023)
                            return 0;
                        if (endDate.Year > 2023)
                            return (new DateTime(2023, 12, 31) - startDate).TotalDays;
                        return (endDate - startDate).TotalDays;
                    }),

                    BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Comparison of our current " + DataType.Split(":")[0] + " with others.",
                            Values = _requestTourService.GetComparisons(_tab2022Data, DataType.Split(":")[0], DataType.Split(":")[1])
                        }
                    },
                    BarLabels = _requestTourService.GetComparisonLabels(_tab2022Data, DataType.Split(":")[0], DataType.Split(":")[1])
                }
            };
    }
}
