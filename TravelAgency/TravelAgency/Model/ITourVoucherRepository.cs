using System.Collections.ObjectModel;

namespace TravelAgency.Model
{
    internal interface ITourVoucherRepository
    {
        void Add(TourVoucher tourVoucher);
        void DeleteById(int id);
        void UpdateAllVouchers();
        void UseVoucher(int id);
        ObservableCollection<TourVoucher> GetAllAsCollection();
        TourVoucher GetVoucherByTouristId(int touristId);
    }
}
