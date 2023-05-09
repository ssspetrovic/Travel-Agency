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
            var dataType = DataType.Split(":")[0];
            _tabAllData = _requestTourService.GetAllTimeRequestedTour(dataType);
            _tab2023Data = _requestTourService.GetMostRequestedToursByYear(dataType, "2023");
            _tab2022Data = _requestTourService.GetMostRequestedToursByYear(dataType, "2023");
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
            if (Enum.TryParse(month, out Key key) && key >= Key.F1 && key <= Key.F12)
            {
                if (TabsIndex == 0) return;
                int monthNumber = key - Key.F1 + 1;
                var showMonthlyRequestStatsViewModel = new ShowMonthlyRequestStatsViewModel()
                {
                    NumberOfRequests = _requestTourService.GetRequestsByDate("", monthNumber, Tabs[TabsIndex].Title).Sum(tour => tour.NumberOfGuests).ToString(),
                    CurrentMonth = new DateTimeFormatInfo().GetMonthName(monthNumber) + ", " + Tabs[TabsIndex].Title
                };
                var showMonthlyRequestStats = new ShowMonthlyRequestStats(showMonthlyRequestStatsViewModel);
                showMonthlyRequestStats.Show();
            }
        }

        public void TabPressed()
        {
            if (TabsIndex < 2)
            {
                TabsIndex++;
                
            }
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

        public ObservableCollection<CurrentRequestTabs> Tabs =>
            new()
            {
                new CurrentRequestTabs
                {
                    Title = "Overall", 
                    DataType = DataType.Split(":")[0], 
                    DataContent = DataType.Split(":")[1], 
                    NumberOfRequests = "Number of Requests: " + _tabAllData.Sum(tour => tour.NumberOfGuests),
                    BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Comparison of our current " + DataType.Split(":")[0] + " with others.",
                            Values = _requestTourService.GetComparisons(_tabAllData, DataType.Split(":")[0], DataType.Split(":")[1])
                        }
                    },
                    BarLabels = new[] {DataType.Split(":")[1]}

                },
                new CurrentRequestTabs
                { 
                    Title = "2023",
                    DataType = DataType.Split(":")[0],
                    DataContent = DataType.Split(":")[1],
                    NumberOfRequests = "Number of Requests: " + _tab2023Data.Sum(tour => tour.NumberOfGuests),
                    BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Comparison of our current " + DataType.Split(":")[0] + " with others.",
                            Values = _requestTourService.GetComparisons(_tab2023Data, DataType.Split(":")[0], DataType.Split(":")[1])
                        }
                    },
                    BarLabels = new[] {DataType.Split(":")[1]}
                },
                new CurrentRequestTabs 
                { 
                    Title = "2022",
                    DataType = DataType.Split(":")[0],
                    DataContent = DataType.Split(":")[1],
                    NumberOfRequests = "Number of Requests: " + _tab2022Data.Sum(tour => tour.NumberOfGuests),
                    BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Comparison of our current " + DataType.Split(":")[0] + " with others.",
                            Values = _requestTourService.GetComparisons(_tab2022Data, DataType.Split(":")[0], DataType.Split(":")[1])
                        }
                    },
                    BarLabels = new[] {DataType.Split(":")[1]}
                }
            };
    }
}
