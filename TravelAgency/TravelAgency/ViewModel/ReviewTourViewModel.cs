using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class ReviewTourViewModel : GuideViewModel
    {
        private readonly TourRatingService _tourRatingService;
        private readonly TourService _tourService;
        private readonly FinishedTourService _finishedTourService;
        private readonly LocationService _locationService;

        public ReviewTourViewModel()
        {
            _tourRatingService = new TourRatingService();
            _tourService = new TourService();
            _finishedTourService = new FinishedTourService();
            _locationService = new LocationService();
        }

        public string ReviewTourName => "Tour: " + CurrentReviewTour.Name;

        public List<string> Tourists
        {
            get
            {
                var tourists = _finishedTourService.FindFinishedTourByName(CurrentReviewTour.Name!).Tourists;
                var tourNames = new List<string>();
                foreach(var tourist in tourists)
                    tourNames.Add(tourist.Name + ", " + _locationService.GetById(tourist.LocationId) + ", " + tourist.TouristAppearance);
                return tourNames;
            }
        }

        public List<string> Comments => 
            _tourRatingService.GetCommentsByTourId(_tourService.GetByName(CurrentReviewTour.Name!).Id);

        public List<string> Ratings =>
            _tourRatingService.GetRatingsByTourId(_tourService.GetByName(CurrentReviewTour.Name!).Id);

        public string AverageRating => "Average Rating: " + Math.Round(Ratings.Select(r => double.Parse(r.Split(' ')[0])).Sum() / Ratings.Count, 2);
    }
}
