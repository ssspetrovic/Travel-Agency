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

namespace TravelAgency.View.Controls.Owner
{
    /// <summary>
    /// Interaction logic for AccommodationSuggestionView.xaml
    /// </summary>
    public partial class AccommodationSuggestionView : Page
    {
        private readonly AccommodationSuggestionViewModel _viewModel = new();
        public AccommodationSuggestionView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
