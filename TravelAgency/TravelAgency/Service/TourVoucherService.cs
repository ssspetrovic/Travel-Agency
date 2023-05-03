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

        public void UpdateAllVouchers()
        {
            _tourVoucherRepository.UpdateAllVouchers();
        }

        public void UseVoucher(int id)
        {
            _tourVoucherRepository.UseVoucher(id);
        }

        public ObservableCollection<TourVoucher> GetAllAsCollection()
        {
            return _tourVoucherRepository.GetAllAsCollection();
        }

        public ObservableCollection<TourVoucher> GetAllValidAsCollection()
        {
            return _tourVoucherRepository.GetAllValidAsCollection();
        }

        public TourVoucher GetVoucherByTouristId(int touristId)
        {
            return _tourVoucherRepository.GetVoucherByTouristId(touristId);
        }
    }
}
