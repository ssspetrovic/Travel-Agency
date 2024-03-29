﻿using LiveCharts;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Controls.Guide;
using TravelAgency.View;

namespace TravelAgency.ViewModel
{
    public class TourStatsViewModel : HomePageViewModel
    {
        private readonly FinishedTourService _finishedTourService;
        private readonly ObservableCollection<Tour> _tabAllData;
        private readonly ObservableCollection<Tour> _tab2023Data;
        private readonly ObservableCollection<Tour> _tab2022Data;
        private int _tabsIndex;

        public TourStatsViewModel()
        {
            TabTourStats = new MyICommand(TabPressed);
            _finishedTourService = new FinishedTourService();
            _tabAllData = _finishedTourService.GetAllTimeBestTour();
            _tab2023Data = _finishedTourService.GetBestOf2022Tour();
            _tab2022Data = _finishedTourService.GetBestOf2023Tour();
            GetMoreStats = new MyICommand(Stats);
        }

        public MyICommand TabTourStats { get; private set; }
        public MyICommand GetMoreStats { get; private set; }

        public void TabPressed()
        {
            if (TabsIndex < 2)
                TabsIndex++;
            else
                TabsIndex = 0;
        }

        private void Stats()
        {
            var window = Application.Current.Windows.OfType<Guide>().FirstOrDefault();
            var allFinishedTours = new AllFinishedTours();
            allFinishedTours.Show();
            window!.Close();
        }

        public int TabsIndex
        {
            get => _tabsIndex;
            set
            {
                _tabsIndex = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<StatsData> Tabs =>
            new()
            {

                new StatsData
                { Title = "All Tours", Data = _tabAllData, Name = "Best Tour: " + _tabAllData[0].Name, KeyPoints = _tabAllData[0].KeyPoints!, BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Age Group",
                            Values = _finishedTourService.GetAgeGroup(_tabAllData[0]),
                        }
                    }, BarLabels = new [] {"0-18", "19-50", "50+"},  PieChartData = new SeriesCollection
                    {
                        new PieSeries
                        {
                            Title = "Voucher",
                            Values = new ChartValues<ObservableValue> { _finishedTourService.GetVoucherOdds(_tabAllData[0])[0] },
                            DataLabels = true
                        },
                        new PieSeries
                        {
                            Title = "No voucher",
                            Values = new ChartValues<ObservableValue> { _finishedTourService.GetVoucherOdds(_tabAllData[0])[1] },
                            DataLabels = true
                        }
                    } },
                new StatsData { Title = "2023", Data = _tab2023Data, Name = "Best Tour: " + _tab2023Data[0].Name, KeyPoints = _tab2023Data[0].KeyPoints!,  BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Age Group",
                            Values = _finishedTourService.GetAgeGroup(_tab2023Data[0])
                        }
                    }, BarLabels = new [] {"0-18", "19-50", "50+"},  PieChartData = new SeriesCollection
                    {
                        new PieSeries
                        {
                            Title = "Voucher",
                            Values = new ChartValues<ObservableValue> { _finishedTourService.GetVoucherOdds(_tab2023Data[0])[0] },
                            DataLabels = true
                        },
                        new PieSeries
                        {
                            Title = "No voucher",
                            Values = new ChartValues<ObservableValue> { _finishedTourService.GetVoucherOdds(_tab2023Data[0])[1] },
                            DataLabels = true
                        }
                    } },
                new StatsData { Title = "2022", Data = _tab2022Data, Name = "Best Tour: " + _tab2022Data[0].Name, KeyPoints = _tab2022Data[0].KeyPoints!, BarData = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Age Group",
                            Values = _finishedTourService.GetAgeGroup(_tab2022Data[0])
                        }
                    }, BarLabels = new [] {"0-18", "19-50", "50+"},  PieChartData = new SeriesCollection
                    {
                        new PieSeries
                        {
                            Title = "Voucher",
                            Values = new ChartValues<ObservableValue> { _finishedTourService.GetVoucherOdds(_tab2022Data[0])[0] },
                            DataLabels = true
                        },
                        new PieSeries
                        {
                            Title = "No voucher",
                            Values = new ChartValues<ObservableValue> { _finishedTourService.GetVoucherOdds(_tab2022Data[0])[1] },
                            DataLabels = true
                        }
                    } }
            };

    }
}
