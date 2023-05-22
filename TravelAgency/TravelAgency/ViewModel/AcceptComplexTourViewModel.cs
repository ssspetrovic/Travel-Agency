using System.Data;
using System.Linq;
using System.Windows;
using TravelAgency.DTO;
using TravelAgency.Service;
using TravelAgency.View;
using TravelAgency.View.Controls.Guide;

namespace TravelAgency.ViewModel
{
    public class AcceptComplexTourViewModel : HomePageViewModel
    {
        private readonly RequestTourService _requestTourService;
        private readonly TourService _tourService;
        private DataRowView? _acceptTour;
        private string? _requestedTourIds;

        public AcceptComplexTourViewModel(string? requestedIds)
        {
            _requestTourService = new RequestTourService();
            _tourService = new TourService();
            _updateView = "";
            _requestedTourIds = requestedIds;
            TourRequestData = GetTourRequestData();
            CreateAcceptedTourCommand = new MyICommand(CreateCommand);
        }

        private string _updateView;
        public MyICommand CreateAcceptedTourCommand { get; private set; }
        public DataView TourRequestData { get; set; }


        public DataView GetTourRequestData()
        {
            var dt = new DataTable();
            dt = UpdateView == "" ? _requestTourService.GetAllAsDataTable(dt, true) : _requestTourService.UpdateDataTable(dt, UpdateView);

            var filteredDt = dt.Clone();
            
            foreach (DataRow row in dt.Rows)
            {
                var id = row["Id"].ToString();
                if (RequestedTourIds!.Contains(id!))
                    filteredDt.ImportRow(row);
            }

            ConvertTourColumn(filteredDt, "Location_Id", typeof(string), "Location");
            ConvertTourColumn(filteredDt, "Language", typeof(string), "Language");
            return filteredDt.DefaultView;
        }

        public void CreateCommand()
        {
            var selectRequestedTourDateViewModel = AcceptTour != null ? new SelectRequestedTourDateViewModel(AcceptTour["DateRange"].ToString()!) :
                new SelectRequestedTourDateViewModel(TourRequestData[0]["DateRange"].ToString()!);
            var selectRequestedTourDate = new SelectRequestedTourDate(selectRequestedTourDateViewModel);
            selectRequestedTourDate.ShowDialog();
            if (!selectRequestedTourDate.GetConformation()) return;

            if (!_tourService.CheckIfDatesExist(selectRequestedTourDate.GetSelectedDates()))
            {
                var parameters = AcceptTour == null ? TourRequestData[0] : AcceptTour;
                CreateAcceptedTourDto.DateList = selectRequestedTourDate.GetSelectedDates();
                CreateAcceptedTourDto.Location = parameters["Location_id"].ToString()!.Split(", ")[0];
                CreateAcceptedTourDto.Language = parameters["Language"].ToString()!;
                CreateAcceptedTourDto.MaxGuests = parameters["NumberOfGuests"].ToString()!;
                CreateAcceptedTourDto.RequestId = int.Parse(parameters["Id"].ToString()!);

                var currentWindow = Application.Current.Windows.OfType<Guide>().FirstOrDefault();
                var newWindow = new Guide();
                if (newWindow.DataContext is not GuideViewModel guideViewModel) return;
                guideViewModel.CurrentViewModel = new CreateTourViewModel();
                newWindow.Title = "Create Tour";
                newWindow.Show();
                currentWindow!.Close();
            }
            else
                MessageBox.Show("You already have a tour in the selected date range!");

        }

        public string UpdateView
        {
            get => _updateView;
            set
            {
                if (_updateView == value) return;
                _updateView = value;
                OnPropertyChanged(_updateView);
            }
        }

        public DataRowView? AcceptTour
        {
            get => _acceptTour;
            set
            {
                _acceptTour = value;
                OnPropertyChanged();
            }
        }

        public string? RequestedTourIds
        {
            get => _requestedTourIds;
            set
            {
                _requestedTourIds = value;
                OnPropertyChanged();
            }
        }
    }

}
