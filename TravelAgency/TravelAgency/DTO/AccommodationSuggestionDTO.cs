using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.DTO
{
    public class AccommodationSuggestionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public int NumOfReseservations { get; set; }
        public int NumOfOccupiedDays { get; set; }

        public AccommodationSuggestionDTO() { }

        public AccommodationSuggestionDTO(int id, string name, Location location, int numOfReseservations, int numOfOccupiedDays)
        {
            Id = id;
            Name = name;
            Location = location;
            NumOfReseservations = numOfReseservations;
            NumOfOccupiedDays = numOfOccupiedDays;
        }
    }
}
