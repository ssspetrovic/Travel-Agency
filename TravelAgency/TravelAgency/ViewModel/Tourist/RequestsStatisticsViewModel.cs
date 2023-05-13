using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;

namespace TravelAgency.ViewModel.Tourist
{
    internal class RequestsStatisticsViewModel : BaseViewModel
    {
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
            _pieChartDataSeries = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Accepted",
                    Values = new ChartValues<ObservableValue> { new(30 * 0.01) },
                    LabelPoint = chartPoint => $"{Math.Round(chartPoint.Y * 100)}%",
                    DataLabels = true,
                },
                new PieSeries
                {
                    Title = "Denied",
                    Values = new ChartValues<ObservableValue> { new(70 * 0.01) },
                    LabelPoint = chartPoint => $"{Math.Round(chartPoint.Y * 100)}%",
                    DataLabels = true,
                }
            };
        }
    }
}
