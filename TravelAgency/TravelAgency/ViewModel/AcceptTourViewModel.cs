using System.Data;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class AcceptTourViewModel : GuideViewModel
    {
        private readonly RequestTourService _requestTourService;

        public AcceptTourViewModel()
        {
            _requestTourService = new RequestTourService();
            _updateView = "";
            TourRequestData = GetTourRequestData();
        }

        private string _updateView;

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
                OnPropertyChanged();
            }
        }
    }

}
