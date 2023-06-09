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


namespace TravelAgency.View.Controls.Guest1
{
    /// <summary>
    /// Interaction logic for SingleAccommodationView.xaml
    /// </summary>
    public partial class SingleAccommodationView : Page
    {
        public SingleAccommodationView(NavigationService navigationService,  HomeViewModel _viewModel)
        {
            InitializeComponent();
            DataContext = new SingleAccommodationViewModel(navigationService, _viewModel);
        }

    }
}
