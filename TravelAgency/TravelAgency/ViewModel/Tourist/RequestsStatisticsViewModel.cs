using LiveCharts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class RequestsStatisticsViewModel : BaseViewModel
    {
        private readonly TourRequestsStatisticsService _statisticsService;

        private double _averageRequests;
        public double AverageRequests
        {
            get => _averageRequests;
            set
            {
                _averageRequests = value;
                OnPropertyChanged();
            }
        }


        private string? _selectedPieYear;
        public string? SelectedPieYear
        {
            get => _selectedPieYear;
            set
            {
                _selectedPieYear = value;
                OnPropertyChanged();
            }
        }

        private string? _selectedBarYear;
        public string? SelectedBarYear
        {
            get => _selectedBarYear;
            set
            {
                _selectedBarYear = value;
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
            _selectedPieYear = "All years";
            _selectedBarYear = "All years";

            _pieChartDataSeries = _statisticsService.GetAcceptanceSeriesCollection(SelectedPieYear);
            _averageRequests = _statisticsService.GetAverageRequestsByStatus(SelectedPieYear);
            PropertyChanged += OnSelectedPieYearChanged;
        }

        private void OnSelectedPieYearChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedPieYear)) return;
            _pieChartDataSeries = _statisticsService.GetAcceptanceSeriesCollection(SelectedPieYear);
            _averageRequests = _statisticsService.GetAverageRequestsByStatus(SelectedPieYear);
            OnPropertyChanged(nameof(PieChartDataSeries));
            OnPropertyChanged(nameof(AverageRequests));
        }
    }
}
