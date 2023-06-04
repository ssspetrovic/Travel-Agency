using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using TravelAgency.Interface;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Service
{
    public class TourVoucherService
    {
        private readonly ITourVoucherRepository _tourVoucherRepository;

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

        public void AddBonusVouchers()
        {
            var touristService = new TouristService();
            var tourists = touristService.GetAllDto();

            const string description = "Congratulations on 5 completed tours. You have won a voucher!";
            var formattedDate = DateTime.Now.AddMonths(6).ToString("yyyy-MM-dd");
            var formattedDateTime = DateTime.ParseExact(formattedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            foreach (var tourist in tourists.Where(tourist => tourist.CompletedToursCount >= 5))
            {
                Add(new TourVoucher(tourist.TouristId, tourist.TouristUsername, description, formattedDateTime, TourVoucher.VoucherStatus.Valid));
            }
        }
    }
}
