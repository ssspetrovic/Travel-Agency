using System.ComponentModel;
using System.Windows.Data;
using TravelAgency.Repository;

namespace TravelAgency.ViewModel
{
    internal class TourVouchersViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _vouchersCollectionViewSource;

        public TourVouchersViewModel()
        {
            var tourVoucherRepository = new TourVoucherRepository();
            tourVoucherRepository.DeleteExpired();
            _vouchersCollectionViewSource = new CollectionViewSource() { Source = tourVoucherRepository.GetAllAsCollection() };
        }

        public ICollectionView VouchersCollectionSource => _vouchersCollectionViewSource.View;
    }
}
