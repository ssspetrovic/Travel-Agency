using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class RequestListViewModel: BaseViewModel, INotifyPropertyChanged
    {
        private readonly CollectionViewSource _requestCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;

        private static DelayRequestService _delayRequestService = new();

        public RequestListViewModel()
        {
            _requestCollection = new CollectionViewSource
            {
                Source = _delayRequestService.GetAll()
            };
        }
        public ICollectionView RequestSourceCollection => _requestCollection.View;

    }
}
