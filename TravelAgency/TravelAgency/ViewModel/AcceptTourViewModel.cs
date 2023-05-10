using System.Data;
using System.Linq;
using System.Windows;
using TravelAgency.DTO;
using TravelAgency.Service;
using TravelAgency.View;
using TravelAgency.View.Controls.Guide;

namespace TravelAgency.ViewModel
{
    public class AcceptTourViewModel : HomePageViewModel
    {
        private readonly RequestTourService _requestTourService;
        private readonly TourService _tourService;
        private DataRowView? _acceptTour;
        private int _acceptTourIndex;

        public AcceptTourViewModel()
        {
            _requestTourService = new RequestTourService();
            _tourService = new TourService();
            _updateView = "";
            TourRequestData = GetTourRequestData();
            FilterTourCommand = new MyICommand(FilterCommand);
            CreateAcceptedTourCommand = new MyICommand(CreateCommand);
        }

        private string _updateView;
        public MyICommand FilterTourCommand { get; private set; }
        public MyICommand CreateAcceptedTourCommand { get; private set; }

        public void FilterCommand()
        {
            var filterRequests = new FilterRequests();
            filterRequests.Closed += (_, _) =>
            {
                UpdateView = filterRequests.UpdateView();
                if (AuthenticateFilters())
                    TourRequestData = GetTourRequestData();
                OnPropertyChanged(nameof(TourRequestData));
            };
            filterRequests.ShowDialog();
        }

        private bool AuthenticateFilters()
        {
            if (UpdateView != "Empty") return true;

            UpdateView = "";
            TourRequestData = GetTourRequestData();
            return false;
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

        public DataView TourRequestData { get; set; }

        public DataView GetTourRequestData()
        {
            var dt = new DataTable();
            dt = UpdateView == "" ? _requestTourService.GetAllAsDataTable(dt) : _requestTourService.UpdateDataTable(dt, UpdateView);

            ConvertTourColumn(dt, "Location_Id", typeof(string), "Location");
            return dt.DefaultView;
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

        public int AcceptTourIndex
        {
            get => _acceptTourIndex;
            set
            {
                _acceptTourIndex = value;
                OnPropertyChanged();
            }
        }
    }

}
