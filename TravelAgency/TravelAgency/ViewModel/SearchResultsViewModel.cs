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
    internal class SearchResultsViewModel: BaseViewModel, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private CollectionViewSource _collection;


        public SearchResultsViewModel()
        {
            var _service = new LocationService();
            _collection = new CollectionViewSource
            {
                Source = _service.GetAll()
            };
        }

        public CollectionViewSource Collection
        {
            get => _collection;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
