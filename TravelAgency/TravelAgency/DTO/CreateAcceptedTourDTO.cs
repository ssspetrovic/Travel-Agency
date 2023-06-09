namespace TravelAgency.DTO
{
    public static class CreateAcceptedTourDto
    {
        public static string Location { get; set; } = string.Empty;
        public static string MaxGuests { get; set; } = string.Empty;
        public static string Language { get; set; } = string.Empty;
        public static string DateList { get; set; } = string.Empty;
        public static int RequestId { get; set; } = -1;
        public static bool IsFromStatistics { get; set; } = false;
    }
}
