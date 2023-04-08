using System.Collections.ObjectModel;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class TourVoucherService
    {
        private readonly TourVoucherRepository _tourVoucherRepository;

        public TourVoucherService()
        {
            _tourVoucherRepository = new TourVoucherRepository();
        }

        public void Add(TourVoucher tourVoucher)
        {
            _tourVoucherRepository.Add(tourVoucher);
        }

        public void DeleteById(int id)
        {
            _tourVoucherRepository.DeleteById(id);
        }

        public void DeleteExpired()
        {
            _tourVoucherRepository.DeleteExpired();
        }

        public ObservableCollection<TourVoucher> GetAllAsCollection()
        {
            return _tourVoucherRepository.GetAllAsCollection();
        }
    }
}
