using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using TravelAgency.Model;
using TravelAgency.Service;
using TravelAgency.View.Controls.Guide;

namespace TravelAgency.ViewModel
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly TourService _tourService;
        private readonly LocationService _locationService;
        private DataRowView? _selectedTour;
        private int _selectedTourIndex;
        private string _averageRating;
        private string _numberOfFinishedTours;

        public MyICommand GetPictures { get; private set; }
        public MyICommand TabPressedCommand { get; private set; }

        public HomePageViewModel()
        {
            _locationService = new LocationService();
            _tourService = new TourService();
            var finishedTourService = new FinishedTourService();
            var userService = new UserService();
            GetPictures = new MyICommand(Picture);
            _selectedTour = null;
            TabPressedCommand = new MyICommand(TabPressed);
            var averageRating = finishedTourService.GetAverageRating(CurrentUser.Username!);
            var numberOfFinishedTours = finishedTourService.GetNumberOfToursByUsername(CurrentUser.Username!);
            _averageRating = "Guide's average rating: " + averageRating;
            _numberOfFinishedTours = "Number of finished tours: " + numberOfFinishedTours;
            userService.SetSuperGuide(CurrentUser.Username, averageRating >= 4.5 && numberOfFinishedTours >= 20);
        }

        public string NumberOfFinishedTours
        {
            get => _numberOfFinishedTours;
            set
            {
                _numberOfFinishedTours = value;
                OnPropertyChanged();
            }
        }

        public string AverageRating
        {
            get => _averageRating;
            set
            {
                _averageRating = value;
                OnPropertyChanged();
            }
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

        public void Picture()
        {
            ParseLinks();
        }

        public void ParseLinks()
        { 
            var images = SelectedTour != null ? _tourService.GetByName(SelectedTour["Name"].ToString()).Photos : _tourService.GetByName(Tours[0]["Name"].ToString()).Photos;
            var links = images.Split(", ");
            foreach (var link in links)
            {
                if (Uri.TryCreate(link, UriKind.Absolute, out var uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {

                    var popupWindow = new PopupPictures();
                    popupWindow.DataContext = new PopupPicturesViewModel { CurrentImageSource = new BitmapImage(uriResult) };
                    popupWindow.ShowDialog();
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
