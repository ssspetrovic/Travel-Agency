using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;

namespace TravelAgency.ViewModel
{
    public class SingleForumOwnerViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _commentCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private Comment? _selectedComment;
        private bool _isCommentSelected;
        public ICollectionView CommentSourceCollection => _commentCollection.View;

        public SingleForumOwnerViewModel(int forumId)
        {
            CommentRepository commentRepository = new CommentRepository();

            _commentCollection = new CollectionViewSource
            {
                Source = commentRepository.GetByForumId(forumId)
            };

        }

        public bool IsCommentSelected
        {
            get => _isCommentSelected;
            set
            {
                _isCommentSelected = value;
                OnPropertyChanged();
            }
        }

        public Comment? SelectedComment
        {
            get => _selectedComment;

            set
            {
                _selectedComment = value;
                IsCommentSelected = true;
                OnPropertyChanged();
            }
        }
    }
}
