using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel;

public class SelectedFinishedTourViewModel : GuideViewModel
{

    private readonly FinishedTourRepository _finishedTourRepository;
    private readonly FinishedTour _currentFinishedTour;
    private readonly TourVoucherRepository _voucherRepository;

    public SelectedFinishedTourViewModel()
    {
        _finishedTourRepository = new FinishedTourRepository();
        _currentFinishedTour = _finishedTourRepository.FindFinishedTour(CurrentFinishedTour.Name!);
        _voucherRepository = new TourVoucherRepository();
    }
    
    public TabData FinishedTourStats => new()
    {
        Name = "You selected: " + _currentFinishedTour.Name,
        KeyPoints = _currentFinishedTour.KeyPoints!,
        Tourists = _currentFinishedTour.Tourists.Select(t => new TouristInfo
        {
            Name = t.UserName,
            Age = t.Age,
            Voucher = _voucherRepository.GetVoucherByTourist(t.Id).Description.Contains("Valid Voucher") ? "Has a voucher" : "Doesn't have a voucher"
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