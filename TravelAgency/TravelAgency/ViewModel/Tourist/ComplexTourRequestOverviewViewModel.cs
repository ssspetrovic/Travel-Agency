using System.Collections.ObjectModel;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class ComplexTourRequestOverviewViewModel : BaseViewModel
    {
        private ObservableCollection<RequestTour> _tourParts;
        public ObservableCollection<RequestTour> TourParts
        {
            get => _tourParts;
            set
            {
                _tourParts = value;
                OnPropertyChanged();
            }
        }

        private int IdCounter { get; set; }

        public ComplexTourRequestOverviewViewModel(int requestId)
        {
            var tourRequestService = new RequestTourService();

            _tourParts = tourRequestService.GetAllAsCollectionByComplexId(CurrentUser.Username, requestId);
            _tourParts = new ObservableCollection<RequestTour>(
                _tourParts.Select(request =>
                {
                    if (string.IsNullOrEmpty(request.AcceptedDate))
                    {
                        request.AcceptedDate = "/";
                    }

                    request.Id = ++IdCounter;
                    return request;
                }));
        }
    }
}
