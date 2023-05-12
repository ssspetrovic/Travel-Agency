using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Forms;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.ViewModel.Tourist;

namespace TravelAgency.Service
{
    internal class MyTourDtoService
    {
        private readonly ActiveTourService _activeTourService;
        private readonly MyTourDtoRepository _myTourDtoRepository;

        public MyTourDtoService()
        {
            _activeTourService = new ActiveTourService();
            _myTourDtoRepository = new MyTourDtoRepository();
        }

        public void Add(Tour tour)
        {
            var status = GetTourStatus(tour);
            var keyPoint = _activeTourService.GetCurrentKeyPointByName(tour.Name);
            var date = DateTime.ParseExact(tour.Date.Split(',')[0].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            _myTourDtoRepository.Add(new MyTourDto(tour.Id, CurrentUser.Username, tour.Name, tour.Location, date, status, keyPoint));
        }

        public void UpdateStatus(string tourName, MyTourDto.TourStatus newStatus)
        {
            _myTourDtoRepository.UpdateStatus(tourName, newStatus);
        }

        public void UpdateKeyPoint(string tourName, string newKeyPoint)
        {
            _myTourDtoRepository.UpdateKeyPoint(tourName, newKeyPoint);
        }

        public ObservableCollection<MyTourDto> GetAllAsCollection()
        {
            return _myTourDtoRepository.GetAllAsCollection();
        }

        private MyTourDto.TourStatus GetTourStatus(Tour tour)
        {
            return _activeTourService.ExistsByName(tour.Name) ? MyTourDto.TourStatus.Active : MyTourDto.TourStatus.Inactive;
        }

        public bool CanRateTour(MyTourDto? selectedTour)
        {
            if (selectedTour == null)
                return false;

            if (selectedTour.Status != MyTourDto.TourStatus.Finished)
                return false;

            var tourRatingService = new TourRatingService();
            return tourRatingService.IsTourRateable(CurrentUser.Username, selectedTour!.Name);
        }

        public bool CanJoinTour(MyTourDto? selectedTour)
        {
            if (selectedTour == null)
                return false;

            return selectedTour.Status == MyTourDto.TourStatus.Active;
        }

        public void JoinTour(MyTourDto? selectedTour)
        {
            if (selectedTour == null)
            {
                MessageBox.Show("No tour selected!", "Error");
                return;
            }

            if (selectedTour.Status != MyTourDto.TourStatus.Active)
            {
                MessageBox.Show("Cannot join this tour!", "Error");
                return;
            }

            UpdateStatus(selectedTour.Name, MyTourDto.TourStatus.Requested);

            var touristService = new TouristService();
            touristService.JoinTour(CurrentUser.Username, selectedTour.TourId, selectedTour.Location.City);
        }
    }
}
