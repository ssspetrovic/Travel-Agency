using System.Diagnostics;
using System.Windows.Forms;

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

        public string TourName { get; }

        public RateTourViewModel(string tourName)
        {
            TourName = tourName;

            if (TourName == "/")
            {
                MessageBox.Show("Error with selected tour!", "Error");
                return;
            }

            TourName = $"- {TourName} -";
        }

        public void Submit()
        {
            Debug.WriteLine(TourName);
            Debug.WriteLine(GuideKnowledgeGrade);
            Debug.WriteLine(GuideLanguageGrade);
            Debug.WriteLine(TourInterestingnessGrade);
            Debug.WriteLine(Comment);
            Debug.Write(PhotoUrls);

            if (GuideKnowledgeGrade == 0 || GuideLanguageGrade == 0 || TourInterestingnessGrade == 0)
            {
                MessageBox.Show("Please select ratings properly!", "Error");
                return;
            }

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
