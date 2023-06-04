namespace TravelAgency.DTO
{
    internal class TouristCountersDto
    {
        public string? TouristUsername { get; set; }
        public int CompletedToursCount { get; set; }

        public TouristCountersDto(string? touristUsername, int completedToursCount)
        {
            TouristUsername = touristUsername;
            CompletedToursCount = completedToursCount;
        }
    }
}
