using System.Data;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class RequestTourService
    {
        private readonly RequestTourRepository _requestTourRepository;

        public RequestTourService()
        {
            _requestTourRepository = new RequestTourRepository();
        }

        public DataTable GetAllAsDataTable(DataTable dt)
        {
            return _requestTourRepository.GetAllAsDataTable(dt);
        }
    }
}
