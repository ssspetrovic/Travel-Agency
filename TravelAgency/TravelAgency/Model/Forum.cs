using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class Forum
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public bool IsClosed { get; set; } = false;
        public int GuestCommentNumber { get; set; } = 0;
        public int OwnerCommentNumber { get; set; } = 0;

        public Forum() { }

        public Forum(int guestId, bool isClosed, int guestCommentNumber, int ownerCommentNumber)
        {
            GuestId = guestId;
            IsClosed = isClosed;
            GuestCommentNumber = guestCommentNumber;
            OwnerCommentNumber = ownerCommentNumber;
        }
    }
}
