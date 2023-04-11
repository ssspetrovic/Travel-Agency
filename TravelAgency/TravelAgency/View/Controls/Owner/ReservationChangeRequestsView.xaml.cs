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
using System.Windows.Shapes;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for ReservationChangeRequestsView.xaml
    /// </summary>
    public partial class ReservationChangeRequestsView : Window
    {
        private readonly ReservationChangeRequestsViewModel _viewModel = new();
        public ReservationChangeRequestsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var OwnerView = new OwnerView();
            OwnerView.Show();
            Close();
        }
    }
}
