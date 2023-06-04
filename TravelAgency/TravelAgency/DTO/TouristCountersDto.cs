namespace TravelAgency.DTO
{
    public class TouristCountersDto
    {
        public int TouristId { get; set; }
        public string? TouristUsername { get; set; }
        public int CompletedToursCount { get; set; }

        public TouristCountersDto(int touristId, string? touristUsername, int completedToursCount)
        {
            TouristId = touristId;
            TouristUsername = touristUsername;
            CompletedToursCount = completedToursCount;
        }
    }
}
