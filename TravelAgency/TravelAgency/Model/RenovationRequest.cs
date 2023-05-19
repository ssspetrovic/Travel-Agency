using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class RenovationRequest
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public string? Type { get; set; }
        public int AccId { get; set; }
        public DateTime Date { get; set; }

        public RenovationRequest() { }

        public RenovationRequest( string? comment, string? type, int accId, DateTime date)
        {
            Comment = comment;
            Type = type;
            AccId = accId;
            Date = date;
        }
    }
}
