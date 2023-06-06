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

        public ForumOwnerViewModel()
        {

            ForumService forumService = new ForumService();

            _forumCollection = new CollectionViewSource
            {
                Source = forumService.DisplayForums()
            };

        }

        public void BtnTakeALook_Click()
        {
            if (IsForumSelected)
            {
                OwnerMainView mainWindow = null;
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is OwnerMainView)
                    {
                        mainWindow = (OwnerMainView)window;
                        break;
                    }
                }

                Frame mainFrame = mainWindow.mainFrame;
                SingleForumView singleForumView = new SingleForumView(SelectedForum);
                mainFrame.Navigate(singleForumView);
            }
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
