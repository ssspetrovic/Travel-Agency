using System.Collections.ObjectModel;

namespace TravelAgency.Model
{
    internal interface ITourVoucherRepository
    {
        void Add(TourVoucher tourVoucher);
        void DeleteById(int id);
        void DeleteExpired();
        ObservableCollection<TourVoucher> GetAllAsCollection();
    }
}
