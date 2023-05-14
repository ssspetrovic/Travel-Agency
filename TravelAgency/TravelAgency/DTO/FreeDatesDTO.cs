using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.DTO
{
    public class FreeDatesDTO
    {
        public DateTime startDate { get; set; } 
        public DateTime endDate { get; set; } 

        public FreeDatesDTO() { }

        public FreeDatesDTO(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
