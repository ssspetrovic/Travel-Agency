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
using TravelAgency.View;
using TravelAgency.View.Controls.Guest1;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class ForumListViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ForumRepository _repository = new();

        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;

        private Forum _selectedForum;
        private Location _location;
        private CollectionViewSource _forumCollection;

        public RelayCommand EnterForumCommand { get; set; }
        public RelayCommand CreateForumCommand { get; set; }

        public ForumListViewModel(NavigationService navigationService, Location location)
        {
            _navigationService = navigationService;
            _location = location;

            _forumCollection = new CollectionViewSource
            {
                Source = _repository.GetByLocationId(location.Id)
            };

            EnterForumCommand = new RelayCommand(Exacute_EnterForumCommand);
            CreateForumCommand = new RelayCommand(Exacute_CreateForumCommand);
        }

        public Forum SelectedForum
        {
            get => _selectedForum;
            set
            {
                _selectedForum = value;
            }
        }

        public ICollectionView ForumCollectionSource => _forumCollection.View;

        public void Exacute_EnterForumCommand(object parameter)
        {
           _navigationService.Navigate(new SingleForumView(_navigationService, SelectedForum));

        }

        public void Exacute_CreateForumCommand(object parameter)
        {
            Forum forum = new Forum(CurrentUser.Id, false, 0, 0, _location.Id, 0);
            _repository.Add(forum);
            _navigationService.Navigate(new SingleForumView(_navigationService, forum));
            //todo navigate to the createdForum
        }

    }
}
