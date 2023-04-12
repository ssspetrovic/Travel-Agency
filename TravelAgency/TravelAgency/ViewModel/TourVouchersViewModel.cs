using System.ComponentModel;
using System.Windows.Data;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    internal class TourVouchersViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _vouchersCollectionViewSource;

        public TourVouchersViewModel()
        {
            var tourVoucherService = new TourVoucherService();
            //tourVoucherService.DeleteExpired();
            _vouchersCollectionViewSource = new CollectionViewSource() { Source = tourVoucherService.GetAllAsCollection() };
        }

        public ICollectionView VouchersCollectionSource => _vouchersCollectionViewSource.View;
    }
}
