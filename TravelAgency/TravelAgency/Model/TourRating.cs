namespace TravelAgency.Model
{
    public enum Grade
    {
        One = 1,
        Two,
        Three,
        Four,
        Five
    }

    internal class TourRating
    {
        public int Id { get; }
        public int TouristId { get; set; }
        public int TourId { get; set; }
        public Grade GuideKnowledgeGrade { get; set; }
        public Grade GuideLanguageGrade { get; set; }
        public Grade TourInterestingnessGrade { get; set; }
        public string Comment { get; set; } = null!;
        public string Photos { get; set; } = null!;

        public TourRating(int touristId, int tourId, Grade guideKnowledgeGrade, Grade guideLanguageGrade, Grade tourInterestingnessGrade, string comment, string photos)
        {
            TouristId = touristId;
            TourId = tourId;
            GuideKnowledgeGrade = guideKnowledgeGrade;
            GuideLanguageGrade = guideLanguageGrade;
            TourInterestingnessGrade = tourInterestingnessGrade;
            Comment = comment;
            Photos = photos;
        }

        public TourRating(int touristId, int tourId, Grade guideKnowledgeGrade, Grade guideLanguageGrade, Grade tourInterestingnessGrade)
        {
            TouristId = touristId;
            TourId = tourId;
            GuideKnowledgeGrade = guideKnowledgeGrade;
            GuideLanguageGrade = guideLanguageGrade;
            TourInterestingnessGrade = tourInterestingnessGrade;
        }
    }
}
