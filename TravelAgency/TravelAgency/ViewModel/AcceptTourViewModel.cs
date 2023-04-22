using System.Data;
using System.Windows;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class AcceptTourViewModel : GuideViewModel
    {
        private readonly RequestTourService _requestTourService;

        public AcceptTourViewModel()
        {
            _requestTourService = new RequestTourService();
        }

        public DataView TourRequestData
        {
            get
            {
                var dt = new DataTable();
                dt = _requestTourService.GetAllAsDataTable(dt);
                ConvertTourColumn(dt, "Location_Id", typeof(string), "Location");

                return dt.DefaultView;
            }
        }
    }
}
