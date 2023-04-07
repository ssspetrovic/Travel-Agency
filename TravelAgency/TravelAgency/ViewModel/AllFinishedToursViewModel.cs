using System.Data;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    public class AllFinishedToursViewModel : GuideViewModel
    {

        private readonly FinishedTourRepository _finishedTourRepository;

        public AllFinishedToursViewModel()
        {
            _finishedTourRepository = new FinishedTourRepository();
        }

        public DataView FinishedTours
        {
            get
            {
                var dt = new DataTable();
                dt = _finishedTourRepository.GetAllAsDataTable(dt);
                ConvertTourColumn(dt, "KeyPointsList", typeof(string), "KeyPoints");

                return dt.DefaultView;
            }
        }

    }
}
