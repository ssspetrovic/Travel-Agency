using System.Collections.ObjectModel;
using TravelAgency.Model;

namespace TravelAgency.Interface
{
    public interface ITourVoucherRepository
    {
        void Add(TourVoucher tourVoucher);
        void DeleteById(int id);
        void UpdateAllVouchers();
        void UseVoucher(int id);
        ObservableCollection<TourVoucher> GetAllAsCollection();
        ObservableCollection<TourVoucher> GetAllValidAsCollection();
        TourVoucher GetVoucherByTouristId(int touristId);
    }
}
