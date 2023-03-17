using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class TouristModel : UserModel
    {
        public List<LocationModel> KeyPoints { get; set; }

        public TouristModel(List<LocationModel> keyPoints)
        {
            KeyPoints = keyPoints;
        }

        public TouristModel(string username, string password, string email, string name, string surname, Role role, List<LocationModel> keyPoints) : base(username, password, email, name, surname, role)
        {
            KeyPoints = keyPoints;
        }

    }
}
