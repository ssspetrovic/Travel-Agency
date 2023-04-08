using System.Data;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class AllFinishedToursViewModel : GuideViewModel
    {

        private readonly FinishedTourService _finishedTourService;

        public AllFinishedToursViewModel()
        {
            _finishedTourService = new FinishedTourService();
        }

        public DataView FinishedTours
        {
            get
            {
                var dt = new DataTable();
                dt = _finishedTourService.GetAllAsDataTable(dt);
                ConvertTourColumn(dt, "KeyPointsList", typeof(string), "KeyPoints");

                return dt.DefaultView;
            }
        }

    }
}
