using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Linq;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Controls.Guide;
using TravelAgency.View;

namespace TravelAgency.ViewModel;

public class SelectedFinishedTourViewModel : HomePageViewModel
{

    private readonly FinishedTourService _finishedTourService;
    private readonly Tour _currentFinishedTour;
    private readonly TourVoucherService _voucherService;
    private string _currentFinishedTourName;

    public SelectedFinishedTourViewModel(string tourName)
    {
        _finishedTourService = new FinishedTourService();
        _voucherService = new TourVoucherService();
        NavCommand = new MyICommand<string>(OnNav);
        _currentFinishedTourName = tourName;
        _currentFinishedTour = _finishedTourService.FindFinishedTourByName(CurrentFinishedTourName);
    }

    public string LoadCurrentUserData => "Welcome " + CurrentUser.DisplayName;

    public MyICommand<string> NavCommand { get; private set; }

    public void OnNav(string command)
    {
        var window = Application.Current.Windows.OfType<SelectedFinishedTour>().FirstOrDefault();
       
        switch (command)
        {
            case "Home Page":
                var mainWindow = new Guide();
                mainWindow.Title = "Home Page";
                if (mainWindow.DataContext is not GuideViewModel guideViewModel) return;
                guideViewModel.CurrentViewModel = new HomePageViewModel();
                mainWindow.Title = command;
                mainWindow.Show();
                window!.Close();
                break;
            case "All Finished Tours":
                var allFinishedTours = new AllFinishedTours();
                allFinishedTours.Title = command;
                allFinishedTours.Show();
                window!.Close();
                break;
        }

    }

    public string CurrentFinishedTourName
    {
        get => _currentFinishedTourName;
        set
        {
            _currentFinishedTourName = value;
            OnPropertyChanged();
        }
    }

    public StatsData FinishedTourStats => new()
    {
        Name = "You selected: " + _currentFinishedTour.Name,
        KeyPoints = _currentFinishedTour.KeyPoints!,
        Tourists = _currentFinishedTour.Tourists.Select(t => new TouristInfo
        {
            Name = t.UserName,
            Age = t.Age,
            Voucher = _voucherService.GetVoucherByTouristId(t.Id).TouristUsername == t.UserName ? "Has a voucher" : "Doesn't have a voucher"
        }).ToList(),
        BarData = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Age Group",
                Values = _finishedTourService.GetAgeGroup(_currentFinishedTour)
            }
        },
        BarLabels = new[] { "0-18", "19-50", "50+" },
        PieChartData = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Voucher",
                Values = new ChartValues<ObservableValue> { _finishedTourService.GetVoucherOdds(_currentFinishedTour)[0] },
                DataLabels = true
            },
            new PieSeries
            {
                Title = "No voucher",
                Values = new ChartValues<ObservableValue> { _finishedTourService.GetVoucherOdds(_currentFinishedTour)[1] },
                DataLabels = true
            }
        }
    };
}