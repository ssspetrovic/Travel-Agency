using LiveCharts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class RequestsStatisticsViewModel : BaseViewModel
    {
        private readonly TourRequestsStatisticsService _statisticsService;
        private string? _selectedYear;

        public string? SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _yearsCollection;
        public ObservableCollection<string> YearsCollection
        {
            get => _yearsCollection;
            set
            {
                _yearsCollection = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection _pieChartDataSeries;

        public SeriesCollection PieChartDataSeries
        {
            get => _pieChartDataSeries;
            set
            {
                _pieChartDataSeries = value;
                OnPropertyChanged();
            }
        }

        public RequestsStatisticsViewModel()
        {
            _statisticsService = new TourRequestsStatisticsService();
            var requestService = new RegularTourRequestService();
            _yearsCollection = requestService.GetAllYears();
            _yearsCollection.Add("All years");
            _selectedYear = "All years";

            _pieChartDataSeries = _statisticsService.GetAcceptanceSeriesCollection(SelectedYear);
            PropertyChanged += OnSelectedYearChanged;
        }

        private void OnSelectedYearChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedYear)) return;
            _pieChartDataSeries = _statisticsService.GetAcceptanceSeriesCollection(SelectedYear);
            OnPropertyChanged(nameof(PieChartDataSeries));
        }
    }
}
