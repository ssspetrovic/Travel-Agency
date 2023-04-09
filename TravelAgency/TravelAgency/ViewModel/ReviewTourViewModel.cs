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

        public ReviewTourViewModel()
        {
            _tourRatingService = new TourRatingService();
            _tourService = new TourService();
        }

        public string ReviewTourName => "Tour: " + CurrentReviewTour.Name;

        public List<string> Tourists =>
            _tourRatingService.GetTouristsByTourId(_tourService.GetByName(CurrentReviewTour.Name!).Id);

        public List<string> Comments => 
            _tourRatingService.GetCommentsByTourId(_tourService.GetByName(CurrentReviewTour.Name!).Id);

        public List<string> Ratings =>
            _tourRatingService.GetRatingsByTourId(_tourService.GetByName(CurrentReviewTour.Name!).Id);

        public string AverageRating => "Average Rating: " + Math.Round(Ratings.Select(r => double.Parse(r.Split(' ')[0])).Sum() / Ratings.Count, 2);
    }
}
