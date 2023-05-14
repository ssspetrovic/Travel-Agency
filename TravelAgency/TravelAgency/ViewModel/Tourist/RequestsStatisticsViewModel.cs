using System.Collections.Generic;
using LiveCharts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class RequestsStatisticsViewModel : BaseViewModel
    {
        private readonly TourRequestsStatisticsService _statisticsService;
        private readonly Dictionary<Language, int> _languageCountDictionary;
        private readonly Dictionary<string, int> _locationCountDictionary;

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

        private SeriesCollection _locationCartesianSeriesCollection;
        public SeriesCollection LocationCartesianSeriesCollection
        {
            get => _locationCartesianSeriesCollection;
            set
            {
                _locationCartesianSeriesCollection = value;
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

        private string[] _locationLabels;
        public string[] LocationLabels
        {
            get => _locationLabels;
            set
            {
                _locationLabels = value;
                OnPropertyChanged();
            }
        }

        public RequestsStatisticsViewModel()
        {
            _statisticsService = new TourRequestsStatisticsService();
            _yearsCollection = _statisticsService.GetAllYearsAsCollection();
            _yearsCollection.Add("All years");
            _selectedPieYear = "All years";
            _selectedCartesianYear = "All years";

            _acceptancePieSeriesCollection = _statisticsService.GetAcceptanceSeriesCollection(SelectedPieYear);
            _averageRequests = _statisticsService.GetAverageRequestsByStatus(SelectedPieYear);
            PropertyChanged += OnSelectedPieYearChanged;

            _languageCountDictionary = _statisticsService.GetLanguageCountDictionary(SelectedCartesianYear);
            _locationCountDictionary = _statisticsService.GetLocationCountDictionary(SelectedCartesianYear);
            _languageCartesianSeriesCollection = _statisticsService.GetLanguageCartesianSeriesCollection(SelectedCartesianYear);
            _locationCartesianSeriesCollection = _statisticsService.GetLocationCartesianSeriesCollection(SelectedCartesianYear);
            _languageLabels = _statisticsService.GetLanguageLabels(_languageCountDictionary);
            _locationLabels = _statisticsService.GetLocationLabels(_locationCountDictionary);
            PropertyChanged += OnSelectedCartesianYearChanged;
        }

        private void OnSelectedPieYearChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedPieYear)) return;
            _acceptancePieSeriesCollection = _statisticsService.GetAcceptanceSeriesCollection(SelectedPieYear);
            _averageRequests = _statisticsService.GetAverageRequestsByStatus(SelectedPieYear);
            OnPropertyChanged(nameof(AcceptancePieSeriesCollection));
            OnPropertyChanged(nameof(AverageRequests));
        }

        private void OnSelectedCartesianYearChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedCartesianYear)) return;
            _languageCartesianSeriesCollection = _statisticsService.GetLanguageCartesianSeriesCollection(SelectedCartesianYear);
            _locationCartesianSeriesCollection = _statisticsService.GetLocationCartesianSeriesCollection(SelectedCartesianYear);
            _languageLabels = _statisticsService.GetLanguageLabels(_languageCountDictionary);
            _locationLabels = _statisticsService.GetLocationLabels(_locationCountDictionary);
            OnPropertyChanged(nameof(LanguageCartesianSeriesCollection));
            OnPropertyChanged(nameof(LocationCartesianSeriesCollection));
            OnPropertyChanged(nameof(LanguageLabels));
            OnPropertyChanged(nameof(LocationLabels));
        }
    }
}
