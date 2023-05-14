using System;
using System.Collections.Generic;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using TravelAgency.Model;

namespace TravelAgency.Service
{
    internal class TourRequestsStatisticsService
    {
        private readonly RegularTourRequestService _tourRequestService;

        public TourRequestsStatisticsService()
        {
            _tourRequestService = new RegularTourRequestService();
        }

        public SeriesCollection GetRequestsSeriesCollection(string? year)
        {
            var requestStats = GetAcceptedRejectedList(year);

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

        private List<int> GetAcceptedRejectedList(string? year)
        {
            var requests = _tourRequestService.GetAllForSelectedYearAsCollection(year);

            var accepted = 0;
            var rejected = 0;

            foreach (var request in requests)
            {
                if (request.Status != RegularTourRequest.TourRequestStatus.Accepted)
                {
                    accepted++;
                }
                else
                {
                    rejected++;
                }
            }

            var total = accepted + rejected;

            if (total > 0)
            {
                accepted = accepted / total * 100;
                rejected = rejected / total * 100;
            }
            else
            {
                throw new DivideByZeroException();
            }

            return new List<int> { accepted, rejected };
        }
    }
}
