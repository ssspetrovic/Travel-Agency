using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Linq;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel;

public class SelectedFinishedTourViewModel : GuideViewModel
{

    private readonly FinishedTourRepository _finishedTourRepository;
    private readonly FinishedTour _currentFinishedTour;

    public SelectedFinishedTourViewModel()
    {
        _finishedTourRepository = new FinishedTourRepository();
        _currentFinishedTour = _finishedTourRepository.FindFinishedTour(CurrentFinishedTour.Name!);
    }
    
    public string SelectedFinishedTourName => CurrentFinishedTour.Name!;

    public TabData FinishedTourStats => new()
    {
        Name = "You selected: " + _currentFinishedTour.Name,
        KeyPoints = _currentFinishedTour.KeyPoints!,
        Tourists = _currentFinishedTour.Tourists.Select(t => new TouristInfo
        {
            Name = t.UserName,
            Age = t.Age,
            Voucher = t.Voucher != TouristVoucher.None ? "Has a voucher" : "Doesn't have a voucher"
        }).ToList(),
        BarData = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Age Group",
                Values = _finishedTourRepository.GetAgeGroup(_currentFinishedTour)
            }
        },
        BarLabels = new[] { "0-18", "19-50", "50+" },
        PieChartData = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Voucher",
                Values = new ChartValues<ObservableValue> { _finishedTourRepository.GetVoucherOdds(_currentFinishedTour)[0] },
                DataLabels = true
            },
            new PieSeries
            {
                Title = "No voucher",
                Values = new ChartValues<ObservableValue> { _finishedTourRepository.GetVoucherOdds(_currentFinishedTour)[1] },
                DataLabels = true
            }
        }
    };
}