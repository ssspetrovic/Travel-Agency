using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TravelAgency.Repository;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for ReservationChangeRequestView.xaml
    /// </summary>
    public partial class ReservationChangeRequestView : Page
    {
        private readonly ReservationChangeRequestsViewModel _viewModel = new();
        public ReservationChangeRequestView()
        {
            InitializeComponent();
            DataContext = _viewModel;
            ChangeColorListView();
        }

        private void ChangeColorListView()
        {
            if(CurrentLanguageAndTheme.themeId == 0)
            {
                RequestListView.Background = Brushes.White;
                RequestListView.Foreground = Brushes.Black;
            }
            else
            {
                RequestListView.Background = Brushes.Black;
                RequestListView.Foreground = Brushes.White;
            }
        }
    }
}
