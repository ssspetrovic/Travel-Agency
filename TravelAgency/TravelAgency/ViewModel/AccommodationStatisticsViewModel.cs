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

        //private FreeDatesDTO? _selectedFreeDate;
        //private bool _isFreeDateSelected;

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

        /*public bool IsFreeDateSelected
        {
            get => _isFreeDateSelected;
            set
            {
                _isFreeDateSelected = value;
                OnPropertyChanged();
            }
        }
        public FreeDatesDTO? SelectedFreeDate
        {
            get => _selectedFreeDate;

            set
            {
                _selectedFreeDate = value;
                IsFreeDateSelected = true;
                OnPropertyChanged();
            }
        }*/

    }
}
