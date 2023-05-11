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
using TravelAgency.Model;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for DisplayGuestReviewsView.xaml
    /// </summary>
    public partial class DisplayGuestReviewsView : Page
    {
        private readonly DisplayGuestsGradesViewModel _viewModel = new();
        public DisplayGuestReviewsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
            ChangeColorListView();
        }

        private void ChangeColorListView()
        {
            if (CurrentLanguageAndTheme.themeId == 0)
            {
                GuestListView.Background = Brushes.White;
                GuestListView.Foreground = Brushes.Black;
            }
            else
            {
                GuestListView.Background = Brushes.Black;
                GuestListView.Foreground = Brushes.White;
            }
        }
    }
}
