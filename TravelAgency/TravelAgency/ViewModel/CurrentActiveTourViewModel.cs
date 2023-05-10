using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class CurrentActiveTourViewModel : HomePageViewModel
    {
        private readonly TouristService _touristService;
        private readonly ActiveTourService _activeTourService;
        private readonly LocationService _locationService;
        private readonly FinishedTourService _finishedTourService;
        private readonly TourService _tourService;
        private int _keyPointsIndex;
        private int _touristsIndex;


        public CurrentActiveTourViewModel()
        {
            _touristService = new TouristService();
            _activeTourService = new ActiveTourService();
            _locationService = new LocationService();
            _finishedTourService = new FinishedTourService();
            _tourService = new TourService();
            CurrentActiveTourCommands = new MyICommand<string>(CurrentActiveTour);
        }


        public MyICommand<string> CurrentActiveTourCommands { get; private set; }

        public void CurrentActiveTour(string command)
        {
            switch (command)
            {
                case "FinishTour":
                    AddFinishedTour();
                    break;
                case "CheckGuests":
                    CheckGuests();
                    break;
                case "TabLists":
                    if (Keyboard.FocusedElement is ListView listView)
                    {
                        if (listView.Name == "ListViewKeyPoints" && KeyPoints.Count > 0)
                        {
                            if (KeyPointsIndex != -1 && KeyPointsIndex < KeyPoints.Count - 1)
                            {
                                KeyPointsIndex++;
                                listView.SelectedIndex = KeyPointsIndex;
                            }
                        }
                    }
                    break;
                case "ChangeData":
                    var keyPoint = KeyPoints[KeyPointsIndex];
                    _activeTourService.UpdateKeyPoint(keyPoint);
                    var allKeyPoints = _activeTourService.GetActiveTourColumn("KeyPointsList").Split(", ");
                    var flag = allKeyPoints.Count(location => location.Contains("False"));
                    OnPropertyChanged(nameof(PassedKeyPoints));

                    var mainWindow = Application.Current.Windows.OfType<View.Guide>().FirstOrDefault();
                    if (mainWindow!.DataContext is GuideViewModel guideViewModel)
                        guideViewModel.CurrentViewModel = new CurrentActiveTourViewModel();
                    if (flag == 0)
                        CurrentActiveTour("FinishTour");
                    break;
            }
        }

        

        private bool CheckPassedKeyPoints(IEnumerable<string> passedKeyPoints)
        {
            var counter = 0;

            foreach (var location in passedKeyPoints)
            {
                if (location.Contains("False"))
                {
                    var question = MessageBox.Show("We haven't passed all key points, are you sure you want to end the tour?", "WAIT", MessageBoxButton.YesNo);
                    if (question == MessageBoxResult.Yes)
                    {
                        counter = 0;
                        break;
                    }

                    counter = 1;
                    var mainWindow = Application.Current.Windows.OfType<View.Guide>().FirstOrDefault();
                    if (mainWindow!.DataContext is GuideViewModel guideViewModel)
                        guideViewModel.CurrentViewModel = new CurrentActiveTourViewModel();
                    break;
                }
            }

            if (counter != 0) return false;
            MessageBox.Show("Tour has been finished!");
            return true;
        }

        public void RemoveTour(List<string> tourDates, IReadOnlyList<string> tourists)
        {

            var firstTourist = _touristService.GetByUsername(tourists[0]);

            if (tourDates.Count < 2)
            {
                foreach (var tourist in tourists)
                {
                    var currentTourist = _touristService.GetByUsername(tourist);
                    _touristService.RemoveTour(currentTourist.Id);
                }
                _tourService.Remove(firstTourist.Tour.Id);
            }
            else
            {
                var dateToday = "";
                foreach (var date in tourDates)
                {
                    if (date.Equals(DateTime.Today.ToString("MM/dd/yyyy", new CultureInfo("en-US"))))
                        dateToday = date;
                }

                foreach (var tourist in tourists)
                {
                    var currentTourist = _touristService.GetByUsername(tourist);
                    _touristService.RemoveTour(currentTourist.Id);
                    if (TouristAppearance.Present != currentTourist.TouristAppearance)
                        _touristService.UpdateAppearance(currentTourist.Id, TouristAppearance.Absent);
                }

                _tourService.RemoveDate(dateToday, tourDates, firstTourist.Tour.Id);
            }
        }

        private void AddFinishedTour()
        {
            var tourists = _activeTourService.GetActiveTourColumn("Tourists").Split(", ");
            var firstTourist = _touristService.GetByUsername(tourists[0]);

            var keyPoints = _activeTourService.GetActiveTourColumn("KeyPointsList");
            var keyPointParts = keyPoints.Split(", ");

            if (!CheckPassedKeyPoints(keyPointParts)) return;

            var tour = _tourService.GetById(firstTourist.Tour.Id);
            var myTourDtoService = new MyTourDtoService();
            myTourDtoService.UpdateStatus(tour.Name, MyTourDto.TourStatus.Finished);

            if (_finishedTourService.CheckExistingTours(tour))
                _finishedTourService.Edit(new FinishedTour(tour.Id, tour.Name, _tourService.GetKeyPoints(string.Join(", ", keyPointParts.Select(p => p[..p.IndexOf(':')]))), _touristService.GetByTour(tour), tour.Date.Split(", ")[0]));
            else
                _finishedTourService.Add(new FinishedTour(tour.Id, tour.Name, _tourService.GetKeyPoints(string.Join(", ", keyPointParts.Select(p => p[..p.IndexOf(':')]))), _touristService.GetByTour(tour), tour.Date.Split(", ")[0]));

            RemoveTour(tour.Date.Split(", ").ToList(), tourists);
            _activeTourService.Remove();
            var mainWindow = Application.Current.Windows.OfType<View.Guide>().FirstOrDefault();
            mainWindow.Title = "Review Tour";
            if (mainWindow!.DataContext is GuideViewModel guideViewModel)
                guideViewModel.CurrentViewModel = new ReviewTourViewModel();
        }

        public void CheckGuests()
        {
            var tourists = _activeTourService.GetActiveTourColumn("Tourists");
            var counter = 0;

            foreach (var tourist in tourists.Split(", "))
            {
                if (_touristService.GetByUsername(tourist).TouristAppearance == TouristAppearance.Unknown)
                    counter++;
            }

            if (counter == 0)
                MessageBox.Show("All tourists were already checked!");
            else
                _touristService.CheckAllTouristAppearances(tourists);

            OnPropertyChanged(nameof(CheckedTourists));
            var mainWindow = Application.Current.Windows.OfType<View.Guide>().FirstOrDefault();
            if (mainWindow!.DataContext is GuideViewModel guideViewModel)
                guideViewModel.CurrentViewModel = new CurrentActiveTourViewModel();
        }

        public int KeyPointsIndex
        {
            get => _keyPointsIndex;
            set
            {
                _keyPointsIndex = value;
                OnPropertyChanged();
            }
        }

        public int TouristsIndex
        {
            get => _touristsIndex;
            set
            {
                _touristsIndex = value;
                OnPropertyChanged();
            }
        }

        public List<string> PassedKeyPoints
        {
            get
            {
                var keyPoints = _activeTourService.GetActiveTourColumn("KeyPointsList");
                var keyPointsList = keyPoints.Split(", ").ToList();
                var passedKeyPoints = new List<string>();

                foreach (var keyPoint in keyPointsList)
                {
                    var city = keyPoint.Split(":");
                    passedKeyPoints.Add(city[1]);
                }

                return passedKeyPoints;
            }
        }

        public string ActiveTourName => _activeTourService.GetActiveTourColumn("Name");

        public List<string> KeyPoints
        {
            get
            {
                var keyPoints = _activeTourService.GetActiveTourColumn("KeyPointsList");
                var keyPointsList = keyPoints.Split(", ").ToList();
                var locations = new List<Location?>();
                var cities = new List<string>();

                foreach (var keyPoint in keyPointsList)
                {
                    var city = keyPoint.Split(":");
                    locations.Add(_locationService.GetById(int.Parse(city[0])));
                }

                foreach (var location in locations)
                {
                    cities.Add(location?.City!);
                }

                return cities;
            }
        }

        public List<string> Tourists
        {
            get
            {
                var tourists = _activeTourService.GetActiveTourColumn("Tourists");
                var touristsList = tourists.Split(", ").ToList();
                return touristsList;
            }
        }

        public List<string> CheckedTourists
        {
            get
            {
                var tourists = _activeTourService.GetActiveTourColumn("Tourists");
                var touristsList = tourists.Split(", ").ToList();

                return touristsList.Select(tourist => _touristService.GetByUsername(tourist).TouristAppearance.ToString()).ToList();
            }
        }

        public List<string> StartingLocation
        {
            get
            {
                var tourists = _activeTourService.GetActiveTourColumn("Tourists");
                var touristsList = tourists.Split(", ").ToList();
                var locations = new List<string>();

                foreach (var tourist in touristsList)
                {
                    var currentTourist = _touristService.GetByUsername(tourist);
                    locations.Add(_locationService.GetById(currentTourist.LocationId)!.City);
                }

                return locations;
            }
        }
    }
}
