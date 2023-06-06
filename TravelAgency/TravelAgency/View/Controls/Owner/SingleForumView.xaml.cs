using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelAgency.DTO;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for SingleForumView.xaml
    /// </summary>
    public partial class SingleForumView : Page
    {
        public SingleForumView()
        {
            InitializeComponent();
        }

        ForumDTO forumDTO;
        public SingleForumView(ForumDTO forum)
        {
            InitializeComponent();
            forumDTO = forum;
            lblLocation.Content = "Location: " + forum.Location;
            lblGuest.Content = "Forum opened by: " + forum.GuestName;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
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
            ForumOwnerView forumOwnerView = new ForumOwnerView();
            mainFrame.Navigate(forumOwnerView);
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            if (txtComment.Text != null)
            {
                CommentRepository commentRepository = new CommentRepository();
                Comment comment = new Comment(CurrentUser.Id, forumDTO.Id, txtComment.Text, CommentType.OwnerAtLocation);
                commentRepository.Add(comment);
            }
        }
    }
}
