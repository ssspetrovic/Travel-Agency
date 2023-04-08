using System;
using System.Collections.Generic;
using System.Data;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class GuideViewModel : BaseViewModel
    {
        private readonly TourService _tourService;
        private readonly LocationService _locationService;

        public GuideViewModel()
        {
            _locationService = new LocationService();
            _tourService = new TourService();
        }


        public string LoadCurrentUserData => "Welcome " + CurrentUser.DisplayName;

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
