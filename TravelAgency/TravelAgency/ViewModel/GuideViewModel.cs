using System;
using System.Collections.Generic;
using System.Data;
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

        private void LoadCurrentUserData()
        {
            //var user = _userRepository.GetByUsername(Environment.UserName);
            //if ( != null)
            //{
            //    CurrentUserAccount.Username = user.UserName;
            //    CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.Surname} ;)";
            //}
            CurrentUserAccount.DisplayName = "Invalid user, not logged in";
            
        }

        private void ExecuteShowGuideViewCommand(object obj)
        {
            CurrentChildView = new GuideViewModel();
            Caption = "Guide";
        }

        public GuideViewModel()
        {
            _locationRepository = new LocationRepository();

            _tourRepository = new TourRepository();

            CurrentUserAccount = new CurrentUserModel();

            ShowGuideViewCommand = new ViewModelCommand(ExecuteShowGuideViewCommand);

            LoadCurrentUserData();
        }

        public DataView Tours
        {
            get
            {
                var dt = new DataTable();
                dt = _tourRepository.GetByAll(dt);

                ConvertColumn(dt, "Location_Id", typeof(string), "Location");
                ConvertColumn(dt, "Language", typeof(string), "Language");
                ConvertColumn(dt, "LocationList", typeof(string), "KeyPoints");

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

                ConvertColumn(dt, "Location_Id", typeof(string), "Location");
                ConvertColumn(dt, "Language", typeof(string), "Language");
                ConvertColumn(dt, "LocationList", typeof(string), "KeyPoints");

                return dt.DefaultView;
            }
        }
        public void ConvertColumn(DataTable dt, string columnName, Type newType, string tourParameter)
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





        public string TourName
        {
            get
            {
                var dt = new DataTable();
                dt = _tourRepository.GetByAll(dt);

                return (string)dt.Rows[0].ItemArray[1]!;
            }
        }

        public DataView ActiveTour
        {
            get
            {
                var dt = new DataTable();
                dt = _tourRepository.GetByAll(dt);
                
                return dt.DefaultView;
            }
        }
    }
}
