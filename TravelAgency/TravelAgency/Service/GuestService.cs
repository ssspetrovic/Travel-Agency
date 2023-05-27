using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.Repository;


namespace TravelAgency.Service
{
    public class GuestService
    {
        private static GuestRepository _repository = new GuestRepository();

        public GuestService() { }
        
        public bool UseDiscountCoin()
        {
            Guest guest = _repository.GetByUserId(CurrentUser.Id);
            UpdateState(guest);
            int credits = guest.Credits;
            if(credits > 0)
            {
                _repository.UpdateCredit(CurrentUser.Id, credits - 1);
                return true;
            }
            return false;
        }

        public void UpdateState(Guest guest)
        {
            if(guest.SuperGuestExpDate != null)
            {
                //TODO: ADD UPDATE STATE
            }
        }


    }
}
