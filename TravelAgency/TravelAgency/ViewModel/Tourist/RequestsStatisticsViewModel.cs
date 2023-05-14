using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class RequestsStatisticsViewModel : BaseViewModel
    {
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
            var requestService = new RegularTourRequestService();
            _yearsCollection = requestService.GetAllYears();
            _yearsCollection.Add("All years");
            _selectedYear = "All years";

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
