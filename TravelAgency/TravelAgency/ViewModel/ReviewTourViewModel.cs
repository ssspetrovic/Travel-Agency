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

        public ReviewTourViewModel()
        {
            _tourRatingService = new TourRatingService();
            _tourService = new TourService();
            _finishedTourService = new FinishedTourService();
            CurrentReviewTour.Name = _finishedTourService.GetNewTourName();
        }

        public string ReviewTourName => "Tour: " + CurrentReviewTour.Name;

        public List<string> Tourists =>
            _tourRatingService.GetTouristsByTourId(_tourService.GetByName(CurrentReviewTour.Name).Id);

        public List<string> Comments => 
            _tourRatingService.GetCommentsByTourId(_tourService.GetByName(CurrentReviewTour.Name).Id);

        public List<string> Ratings =>
            _tourRatingService.GetRatingsByTourId(_tourService.GetByName(CurrentReviewTour.Name).Id);

        public string AverageRating =>
            Ratings.Any(r => !r.EndsWith("⚠️"))
                ? "Average Rating: " + Math.Round(Ratings.Where(r => !r.EndsWith("⚠️")).Select(double.Parse).Average(), 2)
                : "No ratings available";

    }
}
