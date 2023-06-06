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
using TravelAgency.ViewModel;
using TravelAgency.Model;

namespace TravelAgency.View.Controls.Guest1
{
    /// <summary>
    /// Interaction logic for SingleForumView.xaml
    /// </summary>
    public partial class SingleForumView : Page
    {
        public SingleForumView(NavigationService navigationService, Forum forum)
        {
            InitializeComponent();
            DataContext = new SingleForumViewModel(navigationService, forum);
        }
    }
}
