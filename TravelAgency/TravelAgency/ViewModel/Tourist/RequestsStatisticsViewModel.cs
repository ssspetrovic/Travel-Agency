using System.Collections.ObjectModel;
using LiveCharts;

namespace TravelAgency.ViewModel.Tourist
{
    internal class RequestsStatisticsViewModel : BaseViewModel
    {
        private ObservableCollection<ChartValues<double>> _pieChartDataSeries;

        public ObservableCollection<ChartValues<double>> PieChartDataSeries
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
            _pieChartDataSeries = new ObservableCollection<ChartValues<double>>
            {
                new() { 30 },
                new() { 50 }
            };
        }
    }
}
