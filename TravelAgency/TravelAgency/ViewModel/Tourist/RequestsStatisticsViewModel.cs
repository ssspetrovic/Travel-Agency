using LiveCharts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

        private string? _selectedCartesianYear;
        public string? SelectedCartesianYear
        {
            get => _selectedCartesianYear;
            set
            {
                _selectedCartesianYear = value;
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

        private SeriesCollection _acceptancePieSeriesCollection;
        public SeriesCollection AcceptancePieSeriesCollection
        {
            get => _acceptancePieSeriesCollection;
            set
            {
                _acceptancePieSeriesCollection = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection _languageCartesianSeriesCollection;
        public SeriesCollection LanguageCartesianSeriesCollection
        {
            get => _languageCartesianSeriesCollection;
            set
            {
                _languageCartesianSeriesCollection = value;
                OnPropertyChanged();
            }
        }

        private string[] _languageLabels;
        public string[] LanguageLabels
        {
            get => _languageLabels;
            set
            {
                _languageLabels = value;
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
            _selectedCartesianYear = "All years";

            _acceptancePieSeriesCollection = _statisticsService.GetAcceptanceSeriesCollection(SelectedPieYear);
            _averageRequests = _statisticsService.GetAverageRequestsByStatus(SelectedPieYear);
            _languageCartesianSeriesCollection = _statisticsService.GetLanguageCartesianSeriesCollection(null);
            PropertyChanged += OnSelectedPieYearChanged;

            var dict = _statisticsService.GetLanguageCountDictionary(null);
            var labels = _statisticsService.GetLanguageLabels(dict);

            foreach (var pair in dict)
            {
                Debug.WriteLine(pair);
            }
        }

        private void OnSelectedPieYearChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedPieYear)) return;
            _acceptancePieSeriesCollection = _statisticsService.GetAcceptanceSeriesCollection(SelectedPieYear);
            _averageRequests = _statisticsService.GetAverageRequestsByStatus(SelectedPieYear);
            OnPropertyChanged(nameof(AcceptancePieSeriesCollection));
            OnPropertyChanged(nameof(AverageRequests));
        }
    }
}
