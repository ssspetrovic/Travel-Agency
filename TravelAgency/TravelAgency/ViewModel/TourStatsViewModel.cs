using LiveCharts;
using System.Collections.ObjectModel;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    public class TourStatsViewModel : BaseViewModel
    {
        private readonly FinishedTourRepository _finishedTourRepository;
        private readonly TouristRepository _touristRepository;
        private readonly ObservableCollection<Tour> _tabAllData;
        private readonly ObservableCollection<Tour> _tab2023Data;
        private readonly ObservableCollection<Tour> _tab2022Data;



        public TourStatsViewModel()
        {
            _finishedTourRepository = new FinishedTourRepository();
            _touristRepository = new TouristRepository();


            _tabAllData = _finishedTourRepository.GetBestTour();
            _tab2023Data = _finishedTourRepository.GetBestTour();
            _tab2022Data = _finishedTourRepository.GetBestTour();

        }
        
        public string LoadCurrentUserData => "Welcome " + CurrentUser.DisplayName;

        public ObservableCollection<TabData> Tabs =>
            new()
            {

                new TabData
                { Title = "All Tours", Data = _tabAllData, Name = "Best Tour: " + _tabAllData[0].Name, KeyPoints = _tabAllData[0].KeyPoints!, BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Age Group",
                            Values = _finishedTourRepository.GetAgeGroup(_touristRepository.GetByTour(_tabAllData[0]))
                        }
                    }
                    , BarLabels = new [] {"0-18", "19-50", "50+"}, PieChartData = new SeriesCollection
                    {
                        new PieSeries
                        {
                            Title = "Voucher",
                            Values = new ChartValues<ObservableValue> { new(12) },
                            DataLabels = true
                        },
                        new PieSeries
                        {
                            Title = "No voucher",
                            Values = new ChartValues<ObservableValue> { new(13)},
                            DataLabels = true
                        }
                    } },
                new() { Title = "2023", Data = _tab2023Data, Name = "Best Tour: " + _tab2023Data[0].Name, KeyPoints = _tab2023Data[0].KeyPoints! },
                new() { Title = "2022", Data = _tab2022Data, Name = "Best Tour: " + _tab2022Data[0].Name, KeyPoints = _tab2022Data[0].KeyPoints!}
            };

    }
}
