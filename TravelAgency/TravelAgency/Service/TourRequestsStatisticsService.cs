using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using TravelAgency.Model;

namespace TravelAgency.Service
{
    internal class TourRequestsStatisticsService
    {
        private const int PercentageMultiplier = 100;
        private readonly RequestTourService _tourRequestService;

        public TourRequestsStatisticsService()
        {
            _tourRequestService = new RequestTourService();
        }

        public ObservableCollection<string> GetAllYearsAsCollection()
        {
            return _tourRequestService.GetAllYearsAsCollection();
        }

        public SeriesCollection GetAcceptanceSeriesCollection(string? year)
        {
            var requestStats = GetAcceptanceRateForYear(year);

            return new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Accepted",
                    Values = new ChartValues<ObservableValue> { new(requestStats[0]) },
                    LabelPoint = chartPoint => $"{Math.Round(chartPoint.Y)}%",
                    DataLabels = true,
                },
                new PieSeries
                {
                    Title = "Denied",
                    Values = new ChartValues<ObservableValue> { new(requestStats[1]) },
                    LabelPoint = chartPoint => $"{Math.Round(chartPoint.Y)}%",
                    DataLabels = true,
                }
            };
        }

        private (int accepted, int total) GetAcceptedAndTotalRequests(string? year)
        {
            var requests = _tourRequestService.GetAllForSelectedYearAsCollection(year);
            var acceptedRequests = requests.Count(r => r.Status == Status.Accepted);
            return (acceptedRequests, requests.Count);
        }

        private List<int> GetAcceptanceRateForYear(string? year)
        {
            var (accepted, total) = GetAcceptedAndTotalRequests(year);
            if (total == 0)
            {
                throw new DivideByZeroException();
            }

            var acceptanceRate = accepted * PercentageMultiplier / total;
            var rejectionRate = PercentageMultiplier - acceptanceRate;

            return new List<int> { acceptanceRate, rejectionRate };
        }

        public double GetAverageRequestsByStatus(string? year,
            Status status = Status.Accepted)
        {
            var requests = _tourRequestService.GetAllForSelectedYearAsCollection(year);
            var filteredRequests = requests.Where(request => request.Status == status);

            var regularTourRequests = filteredRequests.ToList();
            if (!regularTourRequests.Any()) return 0;

            return double.Round(regularTourRequests.Average(request => request.MaxGuests), 3);
        }

        public Dictionary<Language, int> GetLanguageCountDictionary(string? year)
        {
            var requests = _tourRequestService.GetAllForSelectedYearAsCollection(year);
            var languageCountsDictionary = new Dictionary<Language, int>();

            foreach (var request in requests)
            {
                if (!languageCountsDictionary.ContainsKey(request.Language))
                    languageCountsDictionary.Add(request.Language, 1);
                else
                    languageCountsDictionary[request.Language]++;
            }   

            return languageCountsDictionary;
        }

        public string[] GetLanguageLabels(Dictionary<Language, int> languageCountsDictionary)
        {
            return languageCountsDictionary.Keys.Select(lang => lang.ToString()).ToArray();
        }

        private ChartValues<int> GetLanguageChartValues(string? year)
        {
            var languageCountsDictionary = GetLanguageCountDictionary(year);
            var chartValues = new ChartValues<int>();

            foreach (var pair in languageCountsDictionary)
            {
                chartValues.Add(pair.Value);
            }

            return chartValues;
        }

        public SeriesCollection GetLanguageCartesianSeriesCollection(string? year)
        {
            var values = GetLanguageChartValues(year);

            return new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Requests per language",
                    Values = values,
                    DataLabels = true,
                }
            };
        }

        public Dictionary<string, int> GetLocationCountDictionary(string? year)
        {
            var requests = _tourRequestService.GetAllForSelectedYearAsCollection(year);
            var locationCountDictionary = new Dictionary<string, int>();

            foreach (var request in requests)
            {
                if (!locationCountDictionary.ContainsKey(request.Location.Country))
                    locationCountDictionary.Add(request.Location.Country, 1);
                else
                    locationCountDictionary[request.Location.Country]++;
            }

            return locationCountDictionary;
        }

        public string[] GetLocationLabels(Dictionary<string, int> locationCountsDictionary)
        {
            return locationCountsDictionary.Keys.Select(lang => lang.ToString()).ToArray();
        }

        private ChartValues<int> GetLocationChartValues(string? year)
        {
            var locationCountsDictionary = GetLocationCountDictionary(year);
            var chartValues = new ChartValues<int>();

            foreach (var pair in locationCountsDictionary)
            {
                chartValues.Add(pair.Value);
            }

            return chartValues;
        }

        public SeriesCollection GetLocationCartesianSeriesCollection(string? year)
        {
            var values = GetLocationChartValues(year);

            return new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Requests per location",
                    Values = values,
                    DataLabels = true,
                }
            };
        }
    }
}
