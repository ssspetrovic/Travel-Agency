using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Forms;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.ViewModel;

namespace TravelAgency.Service
{
    internal class MyTourDtoService
    {
        private readonly ActiveTourService _activeTourService;
        private readonly MyToursViewModel? _myToursViewModel; // For accessing view model properties
        private readonly MyTourDtoRepository _myTourDtoRepository;

        public MyTourDtoService()
        {
            _activeTourService = new ActiveTourService();
            _myTourDtoRepository = new MyTourDtoRepository();
        }

        public MyTourDtoService(MyToursViewModel? myToursViewModel)
        {
            _activeTourService = new ActiveTourService();
            _myToursViewModel = myToursViewModel;
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

        public bool IsTourValid()
        {
            if (_myToursViewModel?.SelectedTour!.Status != MyTourDto.TourStatus.Finished)
                return false;

            var tourRatingService = new TourRatingService();
            return CurrentUser.Username != null && tourRatingService.IsTourRateable(CurrentUser.Username, _myToursViewModel?.SelectedTour!.Name);
        }

        public void JoinTour()
        {
            if (_myToursViewModel?.SelectedTour == null)
            {
                MessageBox.Show("No tour selected!", "Error");
                return;
            }

            if (_myToursViewModel?.SelectedTour.Status != MyTourDto.TourStatus.Active)
            {
                MessageBox.Show("Cannot join this tour!", "Error");
                return;
            }

            UpdateStatus(_myToursViewModel.SelectedTour.Name, MyTourDto.TourStatus.Requested);

            var touristService = new TouristService();
            touristService.JoinTour(CurrentUser.Username, _myToursViewModel.SelectedTour.TourId, _myToursViewModel.SelectedTour.Location.City);

            MyToursViewModel.ReloadWindow();
        }
    }
}
