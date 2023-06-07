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
using TravelAgency.View.Controls.Guest1;
using TravelAgency.WindowHelpers;

namespace TravelAgency.ViewModel
{
    public class SingleForumViewModel: BaseViewModel, INotifyPropertyChanged
    {

        private readonly IWindowManager _windowManager;
        private readonly NavigationService _navigationService;

        private readonly ForumRepository _forumRepository = new();
        private readonly CommentRepository _commentRepository = new();
        private readonly ReservationService _reservationService = new();

        private CollectionViewSource _commentCollection;
        private Forum _forum;
        private string _comment;
        private string _isClosed;

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        public SingleForumViewModel(NavigationService navigationService, Forum forum) {
            _windowManager = new WindowManager();
            _navigationService = navigationService;

            if(forum.IsClosed == true)
            {
                _isClosed = "Hidden";
            }
            else
            {
                _isClosed = "Visible";
            }

            _forum = forum;
            _commentCollection = new CollectionViewSource
            {
                Source = _commentRepository.GetByForumId(forum.Id)
            };
            SubmitCommand = new RelayCommand(Exacute_SubmintCommand);
            CloseCommand = new RelayCommand(Exacute_CloseCommand);
        }

        public String IsClosed
        {
            get => _isClosed;
            set
            {
                _isClosed = value;
            }
        }

        public String Comment
        {
            get => _comment;
            set
            {
                _comment = value;
            }
        }
        public ICollectionView CommentSourceCollection => _commentCollection.View;

        public void Exacute_SubmintCommand(object parameter)
        {
            if (_reservationService.HasBeenToLocation(_forum.LocationId))
            {
                _commentRepository.Add(new Model.Comment(CurrentUser.Id, _forum.Id, Comment, CommentType.GuestVisited));
                _forum.GuestCommentNumber++;
                _forumRepository.Update(_forum);
            }
            else
            {
                _commentRepository.Add(new Model.Comment(CurrentUser.Id, _forum.Id, Comment, CommentType.GuestNotVisited));
            }

            _navigationService.Navigate(new SingleForumView(_navigationService, _forum));
        }



        public void Exacute_CloseCommand(object parameter)
        {
            _forum.IsClosed = true;
            _forumRepository.Update(_forum);
            IsClosed = "Hidden";
            _navigationService.Navigate(new SingleForumView(_navigationService, _forum));
        }
    }
}
