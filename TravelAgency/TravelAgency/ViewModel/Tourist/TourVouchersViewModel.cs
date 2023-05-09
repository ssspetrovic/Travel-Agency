using System.ComponentModel;
using System.Windows.Data;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class TourVouchersViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _vouchersCollectionViewSource;

        public TourVouchersViewModel()
        {
            var tourVoucherService = new TourVoucherService();
            _vouchersCollectionViewSource = new CollectionViewSource() { Source = tourVoucherService.GetAllAsCollection() };
        }

        public ICollectionView VouchersCollectionSource => _vouchersCollectionViewSource.View;
    }
}
