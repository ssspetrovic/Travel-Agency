using System.Windows.Navigation;
using TravelAgency.ViewModel;

namespace TravelAgency.View.Controls.Tourist
{
    /// <summary>
    /// Interaction logic for RegularTourRequestView.xaml
    /// </summary>
    public partial class RegularTourRequestView
    {
        public RegularTourRequestView(NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new RegularTourRequestViewModel(navigationService);
        }
    }
}
