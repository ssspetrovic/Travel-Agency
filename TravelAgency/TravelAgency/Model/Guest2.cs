using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal class Guest2 : UserModel
    {
        private List<TourModel> _tours;

        public Guest2()
        {
            _tours = new List<TourModel>();
        }
    }
}
