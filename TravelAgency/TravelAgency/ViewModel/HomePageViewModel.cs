using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly TourService _tourService;
        private readonly LocationService _locationService;
        private DataRowView? _selectedTour;
        private int _selectedTourIndex;
        public bool FocusLocations = true;

        public MyICommand<string> KeyBindings { get; private set; }
        public MyICommand TabPressedCommand { get; private set; }

        public HomePageViewModel()
        {
            _locationService = new LocationService();
            _tourService = new TourService();
            KeyBindings = new MyICommand<string>(Keys);
            _selectedTour = null;
            TabPressedCommand = new MyICommand(TabPressed);
        }

        public DataRowView? SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged();
            }
        }

        public int SelectedTourIndex
        {
            get => _selectedTourIndex;
            set
            {
                _selectedTourIndex = value;
                OnPropertyChanged();
            }
        }

        private void TabPressed()
        {
            if (SelectedTourIndex != -1 && SelectedTourIndex < Tours.Count - 1)
                SelectedTourIndex++;
        }

        public void Keys(string keys)
        {
            switch (keys)
            {
                case "EnterPressed":
                    GetPictures();
                    break;
            }
        }

        public void GetPictures()
        {
            if (SelectedTour == null) return;
            var images = _tourService.GetByName(SelectedTour["Name"].ToString()).Photos;
            var links = images.Split(", ");
            foreach (var link in links)
            {
                if (Uri.TryCreate(link, UriKind.Absolute, out var uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = uriResult.AbsoluteUri,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show($"Invalid link: {link}");
                }
            }
        }

        public DataView Tours
        {
            get
            {
                var dt = new DataTable();
                dt = _tourService.GetAllAsDataTable(dt);

                ConvertTourColumn(dt, "Location_Id", typeof(string), "Location");
                ConvertTourColumn(dt, "Language", typeof(string), "Language");
                ConvertTourColumn(dt, "LocationList", typeof(string), "KeyPoints");

                return dt.DefaultView;
            }
        }

        public string GetKeyPoints(DataRow row, string columnName)
        {
            var keyPointsString = "";

            foreach (var id in row[columnName].ToString()?.Split(", ")!)
            {
                var keyPointId = Convert.ToInt32(id);
                keyPointsString += "; " + _locationService.GetById(keyPointId)?.City + ", " + _locationService.GetById(keyPointId)?.Country;
            }
            keyPointsString = keyPointsString.Substring(2, keyPointsString.Length - 2);

            return keyPointsString;
        }

        public string? GetConvertedRow(string tourParameter, DataRow row, string columnName)
        {
            switch (tourParameter)
            {
                case "Language":
                    return Enum.GetName(typeof(Language), row["Language"]);
                case "Location":
                {
                    var locationId = Convert.ToInt32(row["Location_Id"]);
                    return _locationService.GetById(locationId)?.City + ", " +
                           _locationService.GetById(locationId)?.Country;
                    
                }
                case "KeyPoints":
                    return GetKeyPoints(row, columnName);
                default:
                    return "";
            }
        }

        public void ConvertTourColumn(DataTable dt, string columnName, Type newType, string tourParameter)
        {
            var textList = new List<string?>();

            foreach (DataRow row in dt.Rows)
                textList.Add(GetConvertedRow(tourParameter, row, columnName));

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
    }
}
