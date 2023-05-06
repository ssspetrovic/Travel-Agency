using TravelAgency.Command;

namespace TravelAgency.ViewModel
{
    internal class TouristViewModel
    {
        public BaseCommand<string> NavCommand { get; set; }

        public TouristViewModel()
        {
        }
    }
}
