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
        private readonly TouristService _touristService;

        public ReviewTourViewModel()
        {
            _tourRatingService = new TourRatingService();
            _tourService = new TourService();
            _touristService = new TouristService();
        }

        public string ReviewTourName => "Tour: " + CurrentReviewTour.Name;

        public List<string> Tourists
        {
            get
            {
                var tourists = _touristService.GetByTour(_tourService.GetByName(CurrentReviewTour.Name!));
                var tourNames = new List<string>();
                foreach(var tourist in tourists)
                    tourNames.Add(tourist.Name);
                return tourNames;
            }
        }

        public List<string> Comments => 
            _tourRatingService.GetCommentsByTourId(_tourService.GetByName(CurrentReviewTour.Name!).Id);

        public List<double> Ratings =>
            _tourRatingService.GetRatingsByTourId(_tourService.GetByName(CurrentReviewTour.Name!).Id);

        public string AverageRating => "Average Rating: " + Ratings.Sum() / Ratings.Count;
    }
}
