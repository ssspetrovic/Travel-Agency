using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{

    public enum CommentType
    {
        GuestVisited,
        GuestNotVisited,
        OwnerAtLocation,
        OwnerNotAtLocation
    }
    public class Comment
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int ForumId { get; set; }
        public string? Text { get; set; }
        public CommentType CommentType { get; set; }
        public DateTime Date { get; set; }

        public Comment() { }

        public Comment(int guestId, int forumId, string text, CommentType commentType)
        {
            Date = DateTime.Now;
            GuestId = guestId;
            ForumId = forumId;
            Text = text;
            CommentType = commentType;
        }

        public Comment(int guestId, int forumId, string? text, CommentType commentType, DateTime date) : this(guestId, forumId, text, commentType)
        {
            Date = date;
        }

        public Comment(int id, int guestId, int forumId, string? text, CommentType commentType, DateTime date)
        {
            Id = id;
            GuestId = guestId;
            ForumId = forumId;
            Text = text;
            CommentType = commentType;
            Date = date;
        }
    }
}
