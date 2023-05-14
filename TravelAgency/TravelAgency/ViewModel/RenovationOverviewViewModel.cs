using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class RenovationOverviewViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _previousRenovationsCollection;
        private readonly CollectionViewSource _futureRenovationsCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;

        public ICollectionView PreviousRenovationsSourceCollection => _previousRenovationsCollection.View;
        public ICollectionView FutureRenovationsSourceCollection => _futureRenovationsCollection.View;

        private Renovation? _selectedRenovation;
        private bool _isRenovationSelected;

        private readonly RenovationService _renovationService;
        public RenovationOverviewViewModel()
        {
            _renovationService = new RenovationService();

            _previousRenovationsCollection = new CollectionViewSource
            {
                Source = _renovationService.GetPreviousRenovations()
            };

            _futureRenovationsCollection = new CollectionViewSource
            {
                Source = _renovationService.GetFutureRenovations()
            };
        }

        public bool IsRenovationSelected
        {
            get => _isRenovationSelected;
            set
            {
                _isRenovationSelected = value;
                OnPropertyChanged();
            }
        }
        public Renovation? SelectedRenovation
        {
            get => _selectedRenovation;

            set
            {
                _selectedRenovation = value;
                IsRenovationSelected = true;
                OnPropertyChanged();
            }
        }
    }
}
