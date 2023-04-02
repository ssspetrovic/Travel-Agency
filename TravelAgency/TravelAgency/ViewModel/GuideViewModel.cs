using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    public class GuideViewModel : BaseViewModel
    {
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly ActiveTourRepository _activeTourRepository;
        private readonly TouristRepository _touristRepository;

        public GuideViewModel()
        {
            _locationRepository = new LocationRepository();

            _tourRepository = new TourRepository();

            _activeTourRepository = new ActiveTourRepository();

            _touristRepository = new TouristRepository();

        }


        public string LoadCurrentUserData => "Welcome " + CurrentUser.DisplayName;

        //Sluzi za ispisivanje tura na Home Page
        public DataView Tours
        {
            get
            {
                var dt = new DataTable();
                dt = _tourRepository.GetAllAsDataTable(dt);

                ConvertTourColumn(dt, "Location_Id", typeof(string), "Location");
                ConvertTourColumn(dt, "Language", typeof(string), "Language");
                ConvertTourColumn(dt, "LocationList", typeof(string), "KeyPoints");

                return dt.DefaultView;
            }
        }

        //Sluzi za ispisivanje danasnjih tura na Monitor Tour Page
        public DataView ToursToday
        {
            get
            {
                var dt = new DataTable();
                dt = _tourRepository.GetAllAsDataTable(dt);

                var indexList = new List<int>();

                for (var index = 0; index < dt.Rows.Count; index++)
                {
                    var row = dt.Rows[index];

                    if (!row["Date"].ToString()!.Contains(DateTime.Now.ToString("MM/dd/yyyy", new CultureInfo("en-US"))))
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

        public string GetKeyPoints(DataRow row, string columnName)
        {
            var keyPointsString = "";

            foreach (var id in row[columnName].ToString()?.Split(", ")!)
            {
                var keyPointId = Convert.ToInt32(id);
                keyPointsString += "; " + _locationRepository.GetById(keyPointId)?.City + ", " + _locationRepository.GetById(keyPointId)?.Country;
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
                    return _locationRepository.GetById(locationId)?.City + ", " +
                           _locationRepository.GetById(locationId)?.Country;
                }
                case "KeyPoints":
                    return GetKeyPoints(row, columnName);
                default:
                    return "";
            }
        }

        //Ovo je funkcija koja sluzi za konvertovanje ispisa tura, da ne ispisuju na primer identifikatore, vec podatke koji su validni vodicu
        public void ConvertTourColumn(DataTable dt, string columnName, Type newType, string tourParameter)
        {
            var textList = new List<string?>();

            foreach (DataRow row in dt.Rows)
                textList.Add(GetConvertedRow(tourParameter, row, columnName));

            //Brisemo stari red, kreiramo novi sa istim imenom i ubacujemo zeljene podatke
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

        //Sledecih sest funkcija sluze za ispis parametara tabele ActiveTour
        //Prva je za ispis naziva na vrhu View-a
        //Druga je za ispis kljucnih tacaka
        //Treca je za ispis turista
        //Cetvrta je za ispis booleana koji nam govori da li smo presli kljucnu tacku
        //Peta je za ispis enumeracije da li je turista necekiran, pozvan ili cekiran
        //Sesta je za ispis startne lokacije turiste (odakle je krenuo)
        public string ActiveTourName => _activeTourRepository.GetActiveTour("Name");
        public string DeletedTourName => CancelledTour.Name!;

        public List<string> KeyPoints
        {
            get
            {
                var keyPoints = _activeTourRepository.GetActiveTour("KeyPointsList");
                var keyPointsList = keyPoints.Split(", ").ToList();
                var locations = new List<Location?>();
                var cities = new List<string>();

                foreach (var keyPoint in keyPointsList)
                {
                    var city = keyPoint.Split(":");
                    locations.Add(_locationRepository.GetById(int.Parse(city[0])));
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
                var tourists = _activeTourRepository.GetActiveTour("Tourists");
                var touristsList = tourists.Split(", ").ToList(); 
                return touristsList;
            }
        }

        public List<string> PassedKeyPoints
        {
            get
            {
                var keyPoints = _activeTourRepository.GetActiveTour("KeyPointsList");
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
                var tourists = _activeTourRepository.GetActiveTour("Tourists");
                var touristsList = tourists.Split(", ").ToList();

                return touristsList.Select(tourist => _touristRepository.GetByUsername(tourist).TouristAppearance.ToString()).ToList();
            }
        }

        public List<string> StartingLocation
        {
            get
            {
                var tourists = _activeTourRepository.GetActiveTour("Tourists");
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

        public DataView CancelTours {
            get
            {
                var dt = new DataTable();
                dt = _tourRepository.GetAllAsDataTable(dt);

                var indexList = new List<int>();

                for (var index = 0; index < dt.Rows.Count; index++)
                {
                    var row = dt.Rows[index];
                    var dates = row["Date"].ToString()!.Split(", ");
                    indexList.Add(index);

                    foreach (var date in dates)
                    {
                        var convertedDate = DateTime.Parse(date, new CultureInfo("en-US"));
                        var twoDaysAgo = DateTime.Today.AddDays(2);
                        if (convertedDate < twoDaysAgo) continue;
                        indexList.Remove(index);
                        break;
                    }
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
    }
}
