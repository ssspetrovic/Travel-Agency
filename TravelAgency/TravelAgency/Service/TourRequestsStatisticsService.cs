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
        private Dictionary<Language, int> _languageCount;
        private const int PercentageMultiplier = 100;
        private readonly RegularTourRequestService _tourRequestService;

        public TourRequestsStatisticsService()
        {
            _tourRequestService = new RegularTourRequestService();
            _languageCount = new Dictionary<Language, int>();
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
            var acceptedRequests = requests.Count(r => r.Status == RegularTourRequest.TourRequestStatus.Accepted);
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
            RegularTourRequest.TourRequestStatus status = RegularTourRequest.TourRequestStatus.Accepted)
        {
            var requests = _tourRequestService.GetAllForSelectedYearAsCollection(year);
            var filteredRequests = requests.Where(request => request.Status == status);

            var regularTourRequests = filteredRequests.ToList();
            if (!regularTourRequests.Any()) return 0;

            return double.Round(regularTourRequests.Average(request => request.GuestNumber), 3);
        }

        public Dictionary<Language, int> GetLanguageCountDictionary(string? year)
        {
            // Initializes the dictionary to to available languages and all of the values as 0
            var languageCountsDictionary = Enum.GetValues(typeof(Language))
                .Cast<Language>()
                .ToDictionary(lang => lang, _ => 0);

            var requests = _tourRequestService.GetAllForSelectedYearAsCollection(year);

            foreach (var request in requests)
            {
                languageCountsDictionary[request.Language ?? Language.Unknown]++;
            }

            // Remove all pairs from dictionary where the int 
            return languageCountsDictionary
                .Where(pair => pair.Value != 0)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public string[] GetLanguageLabels(Dictionary<Language, int> languageCountsDictionary)
        {
            return languageCountsDictionary.Keys.Select(lang => lang.ToString()).ToArray();
        }

        public SeriesCollection GetLanguageCartesianSeriesCollection(string? year)
        {
            var values = new ChartValues<int> { 3, 5, 6, 1, 2, 8 };

            return new SeriesCollection
            {
                new RowSeries
                {
                    Title = "test",
                    Values = values,
                    DataLabels = true
                }
            };
        }
    }
}
