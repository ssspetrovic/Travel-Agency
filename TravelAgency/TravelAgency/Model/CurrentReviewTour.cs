using System.Collections.Generic;

namespace TravelAgency.Model
{
    public static class CurrentReviewTour
    {
        public static int Id { get; set; }
        public static string Name { get; set; } = "No Tour";
        public static List<string>? Tourists { get; set; }
        public static List<string>? Comments { get; set; }
        public static List<double>? Ratings { get; set; }
    }
}
