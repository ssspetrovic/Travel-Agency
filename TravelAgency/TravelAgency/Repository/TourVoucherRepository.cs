using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

namespace TravelAgency.Repository
{
    internal class TourVoucherRepository : ITourVoucherRepository
    {
        public void Add(TourVoucher tourVoucher)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteExpired()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<TourVoucher> GetAllAsCollection()
        {
            throw new NotImplementedException();
        }
    }
}
