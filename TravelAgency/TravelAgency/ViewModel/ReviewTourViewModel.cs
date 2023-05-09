using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class ReviewTourViewModel : HomePageViewModel
    {
        private readonly TourRatingService _tourRatingService;
        private readonly TourService _tourService;
        private string _reportedComment;
        private string _reportedTourist;

        public ReviewTourViewModel()
        {
            _tourRatingService = new TourRatingService();
            _tourService = new TourService();
            var finishedTourService = new FinishedTourService();
            CurrentReviewTour.Name = finishedTourService.GetNewTourName();
            ConfirmReportCommand = new MyICommand(ConfirmReport);
            _reportedComment = "";
            _reportedTourist = "";
        }

        public MyICommand ConfirmReportCommand { get; private set; }

        public void ConfirmReport()
        {
            if (ReportedComment.Length == 0)
                if (SelectedTourIndex >= 0)
                    SelectComment(Comments[SelectedTourIndex], Tourists[SelectedTourIndex]);
                else
                    MessageBox.Show("You have no comments selected!");
            else
            {
                _tourRatingService.ReportValidation(ReportedComment, ReportedTourist);
                OnPropertyChanged(nameof(Ratings));
                OnPropertyChanged(nameof(AverageRating));
                SelectComment("", "");
            }
        }

        public void SelectComment(string comment, string tourist)
        {
            ReportedComment = comment;
            ReportedTourist = tourist;
            OnPropertyChanged(ReportedComment);
            OnPropertyChanged(ReportedTourist);
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

        public string ReportedComment
        {
            get => _reportedComment;
            set
            {
                _reportedComment = value;
                OnPropertyChanged();
            }
        }

        public string ReportedTourist
        {
            get => _reportedTourist;
            set
            {
                _reportedTourist = value;
                OnPropertyChanged();
            }
        }

    }
}
