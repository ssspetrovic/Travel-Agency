using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Input;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    public class GuideViewModel : BaseViewModel
    {
        private CurrentUserModel _currentUserAccount = null!;
        private BaseViewModel _currentChildView = null!;
        private string _caption = null!;
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ActiveTourRepository _activeTourRepository;
        private readonly TouristRepository _touristRepository;

        public CurrentUserModel CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value; 
                OnPropertyChanged();
            }
        }

        public BaseViewModel CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged();
            }
        }

        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowGuideViewCommand { get; }

        private void ExecuteShowGuideViewCommand(object obj)
        {
            CurrentChildView = new GuideViewModel();
            Caption = "Guide";
        }

        public GuideViewModel()
        {
            _locationRepository = new LocationRepository();

            _tourRepository = new TourRepository();

            _activeTourRepository = new ActiveTourRepository();

            _touristRepository = new TouristRepository();

            CurrentUserAccount = new CurrentUserModel();

            ShowGuideViewCommand = new ViewModelCommand(ExecuteShowGuideViewCommand);

        }


        public string LoadCurrentUserData
        {
            get
            {
                var currentUserRepository = new CurrentUserRepository();
                return "Welcome " + currentUserRepository.Get().DisplayName;
            }
        }

        public DataView Tours
        {
            get
            {
                var dt = new DataTable();
                dt = _tourRepository.GetByAll(dt);

                ConvertTourColumn(dt, "Location_Id", typeof(string), "Location");
                ConvertTourColumn(dt, "Language", typeof(string), "Language");
                ConvertTourColumn(dt, "LocationList", typeof(string), "KeyPoints");

                return dt.DefaultView;
            }
        }

        public DataView ToursToday
        {
            get
            {
                var dt = new DataTable();
                dt = _tourRepository.GetByAll(dt);

                var indexList = new List<int>();

                for (var index = 0; index < dt.Rows.Count; index++)
                {
                    var row = dt.Rows[index];

                    if (!row["Date"].ToString()!.Contains(DateTime.Now.ToString("MM/dd/yyyy")))
                        indexList.Add(index);
                }

                for (var i = indexList.Count - 1; i >= 0; i--)
                {
                    dt.Rows.RemoveAt(indexList[i]);
                }

                ConvertTourColumn(dt, "Location_Id", typeof(string), "Location");
                ConvertTourColumn(dt, "Language", typeof(string), "Language");
                ConvertTourColumn(dt, "LocationList", typeof(string), "KeyPoints");

                return dt.DefaultView;
            }
        }
        public void ConvertTourColumn(DataTable dt, string columnName, Type newType, string tourParameter)
        {
            var textList = new List<string?>();

            foreach (DataRow row in dt.Rows)
            {
                switch (tourParameter)
                {
                    case "Language":
                        textList.Add(Enum.GetName(typeof(Language), row["Language"]));
                        break;
                    case "Location":
                        var locationId = Convert.ToInt32(row["Location_Id"]);
                        textList.Add(_locationRepository.GetById(locationId)?.City + ", " + _locationRepository.GetById(locationId)?.Country);
                        break;
                    case "KeyPoints":
                        var keyPointsString = "";
                        foreach (var id in row[columnName].ToString()?.Split(", ")!)
                        {
                            var keyPointId = Convert.ToInt32(id);
                            keyPointsString += "; " + _locationRepository.GetById(keyPointId)?.City + ", " + _locationRepository.GetById(keyPointId)?.Country;
                        }

                        keyPointsString = keyPointsString.Substring(2, keyPointsString.Length - 2);
                        textList.Add(keyPointsString);
                        break;
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                row[columnName] = Convert.ChangeType(row[columnName], newType);
            }

            dt.Columns.Remove(columnName);
            var dc = new DataColumn(columnName, newType);
            dt.Columns.Add(dc);
            dc.SetOrdinal(dt.Columns[columnName]!.Ordinal);

            var i = 0;
            foreach (DataRow row in dt.Rows)
            {
                row[columnName] = textList[i++];
            }
        }

        public string ActiveTourName => _activeTourRepository.GetActiveTourData("Name");

        public List<string> KeyPoints
        {
            get
            {
                var keyPoints = _activeTourRepository.GetActiveTourData("KeyPointsList");
                var keyPointsList = keyPoints.Split(", ").ToList();
                var locations = new List<LocationModel?>();
                var cities = new List<string>();

                foreach (var keyPoint in keyPointsList)
                {
                    var city = keyPoint.Split(":");
                    locations.Add(_locationRepository.GetByCity(city[0]));
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
                var tourists = _activeTourRepository.GetActiveTourData("Tourists");
                var touristsList = tourists.Split(", ").ToList(); 
                return touristsList;
            }
        }

        public List<string> PassedKeyPoints
        {
            get
            {
                var keyPoints = _activeTourRepository.GetActiveTourData("KeyPointsList");
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

        public List<string> CheckedTourists
        {
            get
            {
                var tourists = _activeTourRepository.GetActiveTourData("Tourists");
                var touristsList = tourists.Split(", ").ToList();
                var checkedTourists = new List<string>();

                foreach (var tourist in touristsList)
                {
                    checkedTourists.Add(_touristRepository.GetByUsername(tourist).TouristCheck.ToString());
                }

                return checkedTourists;
            }
        }

        public List<string> StartingLocation
        {
            get
            {
                var tourists = _activeTourRepository.GetActiveTourData("Tourists");
                var touristsList = tourists.Split(", ").ToList();
                var locations = new List<string>();

                foreach (var tourist in touristsList)
                {
                    var currentTourist = _touristRepository.GetByUsername(tourist);
                    locations.Add(_locationRepository.GetById(currentTourist.LocationId)!.City);
                }

                return locations;
            }
        }
    }
}
