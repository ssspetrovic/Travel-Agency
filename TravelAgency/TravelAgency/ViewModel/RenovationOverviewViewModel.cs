using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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
    }
}
