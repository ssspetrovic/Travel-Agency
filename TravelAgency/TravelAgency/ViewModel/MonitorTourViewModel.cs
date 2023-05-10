using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System;
using System.Linq;
using System.Windows;
using TravelAgency.Service;
using TravelAgency.DTO;
using TravelAgency.Model;
using MessageBox = System.Windows.Forms.MessageBox;
using TravelAgency.View;

namespace TravelAgency.ViewModel
{
    public class MonitorTourViewModel : HomePageViewModel
    {
        private readonly TourService _tourService;
        private readonly ActiveTourService _activeTourService;
        private readonly TouristService _touristService;
        private bool _isActive;

        public MonitorTourViewModel()
        {
            _tourService = new TourService();
            _activeTourService = new ActiveTourService();
            _touristService = new TouristService();
            ActiveTourCommands = new MyICommand<string>(OnActive);
            SelectedTour = ToursToday.Count > 0 ? ToursToday[0] : null;
        }

        public MyICommand<string> ActiveTourCommands { get; private set; }

        private void OnActive(string command)
        {
            switch (command)
            {
                case "EnterPressed":
                    if(!_isActive)
                        GetActiveTour(false);
                    break;
                case "KeyPPressed":
                    ParseLinks();
                    break;
                case "KeyAPressed":
                    if (_activeTourService.IsActive())
                        GetActiveTour(true);
                    else
                        MessageBox.Show("There is no active tour at the moment!");
                    break;
            }
        }

        private void GetActiveTour(bool needsUpdate)
        {
            if (!_activeTourService.IsActive() || needsUpdate)
            {

                Tour selectedTour;
                if (_activeTourService.IsActive())
                    selectedTour = _tourService.GetByName(_activeTourService.GetActiveTourColumn("Name"));
                else
                {
                    if(SelectedTour == null)
                        selectedTour = _tourService.GetByName(ToursToday[0]["Name"].ToString());
                    else 
                        selectedTour = _tourService.GetByName(SelectedTour!["Name"].ToString());
                }

                var tourists = _touristService.GetByTour(selectedTour);

                var activeKeyPoints = selectedTour.KeyPoints.ToDictionary(location => location!.Id, _ => false);

                var currentKeyPoint = selectedTour.Location;
                _activeTourService.Remove();
                _activeTourService.Add(new ActiveTour(selectedTour.Name, activeKeyPoints, tourists, currentKeyPoint.City));

                var myTourDtoService = new MyTourDtoService();
                myTourDtoService.UpdateStatus(selectedTour.Name, MyTourDto.TourStatus.Active);
                myTourDtoService.UpdateKeyPoint(selectedTour.Name, currentKeyPoint.ToString());
            }

            else
                MessageBox.Show("You already have an active tour!");

            _isActive = true;

            var mainWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault()!.DataContext as GuideViewModel;
            mainWindow!.CurrentViewModel = new CurrentActiveTourViewModel();
        }


        public DataView ToursToday
        {
            get
            {
                var dt = new DataTable();
                dt = _tourService.GetAllAsDataTable(dt);

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

    }
}
