using System;
using System.Collections.ObjectModel;
using System.Globalization;
using TravelAgency.DTO;
using TravelAgency.Model;

namespace TravelAgency.Service
{
    internal class MyTourDtoService
    {
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        private readonly ActiveTourService _activeTourService;

        public MyTourDtoService()
        {
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();
            _activeTourService = new ActiveTourService();
        }

        public ObservableCollection<MyTourDto> GetAllAsCollection()
        {
            var myTours = new ObservableCollection<MyTourDto>();
            var tourReservations = _tourReservationService.GetAllAsCollection();

            foreach (var reservation in tourReservations)
            {
                var tour = _tourService.GetById(reservation.TourId);
                var status = GetTourStatus(tour);
                var keyPoint = _activeTourService.GetCurrentKeyPointByName(tour.Name);
                var date = DateTime.ParseExact(tour.Date.Split(',')[0].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                myTours.Add(new MyTourDto(tour.Name, tour.Location, date, status, keyPoint));
            }

            return myTours;
        }


        private MyTourDto.TourStatus GetTourStatus(Tour tour)
        {
            return _activeTourService.ExistsByName(tour.Name) ? MyTourDto.TourStatus.Active : MyTourDto.TourStatus.Inactive;
        }
    }
}
