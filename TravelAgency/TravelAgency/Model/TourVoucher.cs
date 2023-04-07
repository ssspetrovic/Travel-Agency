namespace TravelAgency.Model
{
    internal class TourVoucher
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

        public TourVoucher(int id, string description, string date)
        {
            Id = id;
            Description = description;
            Date = date;
        }
    }
}
