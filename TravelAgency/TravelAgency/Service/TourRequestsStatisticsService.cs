using System;
using System.Collections.Generic;
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
        private readonly RegularTourRequestService _tourRequestService;

        public TourRequestsStatisticsService()
        {
            _tourRequestService = new RegularTourRequestService();
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

            return regularTourRequests.Average(request => request.GuestNumber);
        }
    }
}
