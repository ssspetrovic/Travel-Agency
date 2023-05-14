using System.Collections.ObjectModel;

namespace TravelAgency.Service
{
    internal class TourRequestsStatisticsService
    {
        private readonly RegularTourRequestService _tourRequestService;

        public TourRequestsStatisticsService()
        {
            _tourRequestService = new RegularTourRequestService();
        }
    }
}
