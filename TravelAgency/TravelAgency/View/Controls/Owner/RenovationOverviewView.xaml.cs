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
using TravelAgency.Service;
using TravelAgency.View.Controls.Guest1;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for RenovationOverviewView.xaml
    /// </summary>
    public partial class RenovationOverviewView : Page
    {
        private readonly RenovationOverviewViewModel _viewModel = new();
        public RenovationOverviewView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
