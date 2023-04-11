using System.Windows.Forms;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    internal class RateTourViewModel : BaseViewModel
    {
        private int _guideKnowledgeGrade;
        private int _guideLanguageGrade;
        private int _tourInterestingnessGrade;
        private string? _comment;
        private string? _photoUrls;
        private string? _url;

        public int GuideKnowledgeGrade
        {
            get => _guideKnowledgeGrade;
            set
            {
                _guideKnowledgeGrade = value;
                OnPropertyChanged();
            }
        }

        public int GuideLanguageGrade
        {
            get => _guideLanguageGrade;
            set
            {
                _guideLanguageGrade = value;
                OnPropertyChanged();
            }
        }

        public int TourInterestingnessGrade
        {
            get => _tourInterestingnessGrade;
            set
            {
                _tourInterestingnessGrade = value;
                OnPropertyChanged();
            }
        }

        public string? Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }
        public string? PhotoUrls
        {
            get => _photoUrls;
            set
            {
                _photoUrls = value;
                OnPropertyChanged();
            }
        }

        public string? Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        public string TourNameHeader { get; }
        private string TourName { get; }

        public RateTourViewModel(string tourName)
        {
            TourName = tourName;

            if (TourNameHeader == "/")
            {
                MessageBox.Show("Error with selected tour!", "Error");
                return;
            }

            TourNameHeader = $"'{tourName}'";
        }

        public void Submit()
        {
            if (GuideKnowledgeGrade == 0 || GuideLanguageGrade == 0 || TourInterestingnessGrade == 0)
            {
                MessageBox.Show("Please select ratings properly!", "Error");
                return;
            }

            var touristService = new TouristService();
            var tourRatingService = new TourRatingService();
            var tourService = new TourService();
            var tourist = touristService.GetByUsername(CurrentUser.Username);

            tourRatingService.Add(new TourRating(
                tourist.Id,
                tourService.GetByName(TourName).Id,
                (Grade)GuideKnowledgeGrade,
                (Grade)GuideLanguageGrade,
                (Grade)TourInterestingnessGrade,
                Comment ?? "",
                PhotoUrls ?? ""
            ));

            var myTourDtoService = new MyTourDtoService();
            myTourDtoService.UpdateStatus(TourName, MyTourDto.TourStatus.Rated);
            MyToursViewModel.ReloadWindow();
        }

        public void AddUrl()
        {
            if (string.IsNullOrEmpty(Url))
            {
                MessageBox.Show("Invalid link!", "Error");
                return;
            }

            var formattedUri = Url.Trim();
            formattedUri = $"{formattedUri};\r\n";
            PhotoUrls += formattedUri;
            Url = string.Empty;
        }
    }
}
