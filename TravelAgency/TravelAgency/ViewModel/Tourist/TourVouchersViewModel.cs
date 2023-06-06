using System.ComponentModel;
using System.Windows.Data;
using TravelAgency.Service;

namespace TravelAgency.ViewModel.Tourist
{
    internal class TourVouchersViewModel : BaseViewModel
    {
        private readonly TouristViewModel _touristViewModel;
        private readonly CollectionViewSource _vouchersCollectionViewSource;
        private bool _isTooltipsSwitchToggled;
        public bool IsTooltipsSwitchToggled
        {
            get => _isTooltipsSwitchToggled;
            set
            {
                _isTooltipsSwitchToggled = value;
                OnPropertyChanged();
            }
        }

        public TourVouchersViewModel(TouristViewModel touristViewModel)
        {
            var tourVoucherService = new TourVoucherService();
            tourVoucherService.AddBonusVouchers();

            _vouchersCollectionViewSource = new CollectionViewSource { Source = tourVoucherService.GetAllAsCollection() };
            _touristViewModel = touristViewModel;
            IsTooltipsSwitchToggled = _touristViewModel.IsTooltipsSwitchToggled;
            _touristViewModel.PropertyChanged += TouristViewModel_PropertyChanged;
        }

        public ICollectionView VouchersCollectionSource => _vouchersCollectionViewSource.View;

        private void TouristViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(TouristViewModel.IsTooltipsSwitchToggled)) return;
            if (sender != null) IsTooltipsSwitchToggled = ((TouristViewModel)sender).IsTooltipsSwitchToggled;
        }
    }
}
