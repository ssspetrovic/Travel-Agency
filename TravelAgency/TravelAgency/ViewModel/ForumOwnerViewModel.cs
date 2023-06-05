using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using TravelAgency.Command;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.Service;
using TravelAgency.View.Controls.Owner;
using TravelAgency.View;
using System.Windows;
using Prism.Commands;

namespace TravelAgency.ViewModel
{
    public class ForumOwnerViewModel : BaseViewModel
    {
        private readonly CollectionViewSource _forumCollection;
        public new event PropertyChangedEventHandler? PropertyChanged;
        private ForumDTO? _selectedForum;
        private bool _isForumSelected;

        public ICollectionView ForumSourceCollection => _forumCollection.View;

        public DelegateCommand BtnTakeALook { get; set; }
        public ForumOwnerViewModel()
        {
            BtnTakeALook = new DelegateCommand(Execute_BtnTakeALook);

            ForumService forumService = new ForumService();

            _forumCollection = new CollectionViewSource
            {
                Source = forumService.DisplayForums()
            };

        }

        private void Execute_BtnTakeALook()
        {
            MessageBox.Show(SelectedForum.GuestName);
        }

        public bool IsForumSelected
        {
            get => _isForumSelected;
            set
            {
                _isForumSelected = value;
                OnPropertyChanged();
            }
        }

        public ForumDTO? SelectedForum
        {
            get => _selectedForum;

            set
            {
                _selectedForum = value;
                IsForumSelected = true;
                OnPropertyChanged();
            }
        }
    }
}
