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
        private static GuestRepository _guest_repository = new GuestRepository();

        public GuestService() { }
        
        public bool UseDiscountCoin()
        {
            Guest guest = _guest_repository.GetByUserId(CurrentUser.Id);
            UpdateState(guest);
            int credits = guest.Credits;
            if(credits > 0)
            {
                _guest_repository.UpdateCredit(CurrentUser.Id, credits - 1);
                return true;
            }
            return false;
        }

        public void UpdateState(Guest guest)
        {
            var _repository = new ReservationRepository();
            if (guest.SuperGuestExpDate != null)
            {
                DateTime expDate = guest.SuperGuestExpDate ?? DateTime.Now;
                if (expDate.AddYears(1) < DateTime.Now)
                {
                    if (_repository.CountBeforeDate(expDate) >= 10)
                    {
                        _guest_repository.MakeSuperGuest(CurrentUser.Id);
                    }
                    else
                    {
                        _guest_repository.ResignSuperGuest(CurrentUser.Id);
                    }

                }
            }

        }


    }
}
