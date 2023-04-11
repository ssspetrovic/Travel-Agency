
using System.Diagnostics;
using System.Windows.Forms;
using TravelAgency.Model;

namespace TravelAgency.ViewModel
{
    internal class RateTourViewModel : BaseViewModel
    {
        private int _guideKnowledgeGrade;
        private int _guideLanguageGrade;
        private int _tourInterestingnessGrade;
        private string? _comment;

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

        public string TourName { get; set; }

        public RateTourViewModel(string tourName)
        {
            TourName = tourName;

            if (TourName == "/")
            {
                MessageBox.Show("Error with selected tour!", "Error");
                return;
            }
            
        }

        public void Submit()
        {
            Debug.WriteLine(TourName);
            Debug.WriteLine(GuideKnowledgeGrade);
            Debug.WriteLine(GuideLanguageGrade);
            Debug.WriteLine(TourInterestingnessGrade);
            Debug.WriteLine(Comment);
        }
    }
}
