using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TravelAgency.DTO;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class AccommodationStatisticsViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _accommodationsCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;

        private AccommodationStatDTO? _selectedStat;
        private bool _isStatSelected;

        private readonly AccommodationService _accommodationService;
        public ICollectionView AccommodationsSourceCollection => _accommodationsCollection.View;
        public AccommodationStatisticsViewModel()
        {
            _accommodationService = new AccommodationService();

            _accommodationsCollection = new CollectionViewSource
            {
                Source = _accommodationService.GetAccommodationNames()
            };
        }

        public bool IsStatSelected
        {
            get => _isStatSelected;
            set
            {
                _isStatSelected = value;
                OnPropertyChanged();
            }
        }
        public AccommodationStatDTO? SelectedStat
        {
            get => _selectedStat;

            set
            {
                _selectedStat = value;
                IsStatSelected = true;
                OnPropertyChanged();
            }
        }

    }
}
