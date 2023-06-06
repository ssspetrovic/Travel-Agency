using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Navigation;
using TravelAgency.Command;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class ForumListViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ForumRepository _repository = new();
        private Location _location;
        private CollectionViewSource _forumCollection;
        public ForumListViewModel(NavigationService navigationService, Location location)
        {
            _location = location;
            _forumCollection = new CollectionViewSource
            {
                Source = _repository.GetByLocationId(location.Id)
            };
        }

        public ICollectionView ForumCollectionSource => _forumCollection.View;

    }
}
