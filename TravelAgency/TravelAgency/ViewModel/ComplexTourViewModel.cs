using System.Data;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class ComplexTourViewModel : HomePageViewModel
    {
        private readonly RequestTourService _requestTourService;
        private DataRowView? _acceptTour;
        private string _updateView;

        public ComplexTourViewModel()
        {
            _requestTourService = new RequestTourService();
            _updateView = "";
            CheckForComplexStatusUpdate();
            ComplexTourRequestData = GetTourRequestData();
        }

        public DataView ComplexTourRequestData { get; set; }
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

        private void CheckForComplexStatusUpdate()
        {
            var complexService = new ComplexTourRequestService();
            complexService.HandleComplexStatuses();
        }

        public DataView GetTourRequestData()
        {
            var dt = new DataTable();
            dt = UpdateView == "" ? _requestTourService.GetAllComplexAsDataTable(dt) : _requestTourService.UpdateComplexDataTable(dt, UpdateView);

            ConvertTourColumn(dt, "Location_Id", typeof(string), "Location");
            ConvertTourColumn(dt, "Language", typeof(string), "Language");
            return dt.DefaultView;
        }

        public DataRowView? ComplexTour
        {
            get => _acceptTour;
            set
            {
                _acceptTour = value;
                OnPropertyChanged();
            }
        }
    }
}
