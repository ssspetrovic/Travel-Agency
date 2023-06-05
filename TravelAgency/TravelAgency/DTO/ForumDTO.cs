using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.DTO
{
    public class ForumDTO
    {
        public int Id { get; set; }
        public int Comments { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public string Closed { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string IsVisited { get; set; } = string.Empty;

        public ForumDTO() { }

        public ForumDTO(int id, int comments, string guestName, string closed, string location, string isVisited)
        {
            Id = id;
            Comments = comments;
            GuestName = guestName;
            Closed = closed;
            Location = location;
            IsVisited = isVisited;
        }
    }
}
