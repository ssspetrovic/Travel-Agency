using System;
using TravelAgency.Model;

namespace TravelAgency.ViewModel
{
    internal class RegularTourRequestViewModel : BaseViewModel
    {
        public Location? Location { get; set; }
        public Language Language { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string? Description { get; set; }
        public Array Locations;

        public RegularTourRequestViewModel()
        {
            Locations = Enum.GetValues(typeof(Language));
        }
    }
}
